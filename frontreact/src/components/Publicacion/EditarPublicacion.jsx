import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import PublicacionForm from "./FormPublicacion";
import Header from "../Header";

export default function EditarPublicacion() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [publicacion, setPublicacion] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:5000/api/Publicacion/byId?id=${id}`)
      .then((res) => res.json())
      .then((data) => setPublicacion(data));
  }, [id]);

  const handleCancel = () => {
    console.log("Cancelado");
    navigate("/HomePage");
  };
  const handleSubmit = async (data, imagenes) => {
    fetch(`http://localhost:5000/api/Publicacion/byId?id=${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al guardar la maquinaria");
        navigate(`/DetallePublicacion/${data.idPublicacion}`);
      })
      .catch((error) => {
        console.error(error);
        alert("No se pudo guardar la maquinaria");
      });

    // // Paso 2: Subir imágenes
    // for (let i = 0; i < imagenes.length; i++) {
    //   const formData = new FormData();
    //   formData.append("EntidadID", data.idPublicacion);
    //   formData.append("TipoEntidad", 0);
    //   formData.append("Nombre", `Imagen ${i + 1}`);
    //   formData.append("Descripcion", `Imagen ${i + 1} de la publicación`);
    //   formData.append("Archivo", imagenes[i]);

    //   const imgRes = await fetch("http://localhost:5000/api/Archivo", {
    //     method: "POST",
    //     body: formData,
    //   });

    //   if (!imgRes.ok) {
    //     console.error(`Error al subir imagen ${i + 1}`);
    //   }
    // }
  };
  if (!publicacion) return <p>Cargando...</p>;

  return (
    <>
      <Header />
      <PublicacionForm
        initialData={publicacion}
        onSubmit={handleSubmit}
        onCancel={handleCancel}
        modo="Editar"
      />
    </>
  );
}
