import { useState } from "react";
import TextInput from "../TextInput";
import SelectInput from "../SelectInput";
import TagSelector from "../TagSelector";
import FormButtons from "../FormButtons";
import ImageUploader from "../ImageUploader";
import ImagePreviewList from "../ImagePreviwList";
import Header from "../Header";
import { useNavigate } from "react-router-dom";
import PublicacionForm from "./FormPublicacion";

export default function CrearPublicacion() {
  const navigate = useNavigate();

  const handleSubmit = async (publicacionData, imagenes) => {
    try {
      // Paso 1: Enviar publicación
      const res = await fetch("http://localhost:5000/api/Publicacion", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(publicacionData),
      });

      if (!res.ok) throw new Error("Error al crear publicación");

      const nuevaPublicacion = await res.json();
      
      // Paso 2: Subir imágenes
      for (const [i, nuevaImg] of imagenes.imagenesNuevas.entries()) {
        const formData = new FormData();
        formData.append("EntidadID", nuevaPublicacion.idPublicacion);
        formData.append("TipoEntidad", 0); // o el que corresponda
        formData.append("Nombre", nuevaImg.name || `Imagen ${i + 1}`);
        formData.append("Descripcion", `Imagen ${i + 1} de la publicación`);
        formData.append("Archivo", nuevaImg);

        try {
          const uploadRes = await fetch("http://localhost:5000/api/Archivo", {
            method: "POST",
            body: formData,
          });
          if (!uploadRes.ok) {
            console.error(
              `Error al subir imagen ${i + 1}: ${uploadRes.statusText}`
            );
          }
        } catch (error) {
          console.error(`Error al subir imagen ${i + 1}:`, error);
        }
      }

      setTimeout(() => {
        navigate(`/DetallePublicacion/${nuevaPublicacion.idPublicacion}`);
      }, 2000);
    } catch (error) {
      console.error("Error al crear publicación:", error);
    }
  };

  const handleCancel = () => {
    console.log("Cancelado");
    navigate("/HomePage");
  };

  return (
    <>
      <Header />
      <PublicacionForm onSubmit={handleSubmit} onCancel={handleCancel} />
    </>
  );
}
