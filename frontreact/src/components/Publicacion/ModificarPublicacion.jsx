import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import PublicacionForm from "./FormPublicacion";
import Header from "../Header";

export default function ModificarPublicacion() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [publicacion, setPublicacion] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:5000/api/Publicacion/byId?id=${id}`)
      .then((res) => res.json())
      .then((data) => {
        fetch(
          `http://localhost:5000/api/Archivo/byEntidad?entidadId=${data.idPublicacion}&tipoEntidad=0`
        )
          .then((res) => res.json())
          .then((imagenesData) => {
            const imagenesMasPublicacion = { ...data, imagenes: imagenesData };
            setPublicacion(imagenesMasPublicacion);
          });
      });
  }, [id]);

  const actualizarArchivo = async ({
    idArchivo,
    nombre,
    descripcion,
    archivo,
  }) => {
    const formData = new FormData();
    formData.append("IdArchivo", idArchivo);
    if (nombre) formData.append("Nombre", nombre);
    if (descripcion) formData.append("Descripcion", descripcion);
    if (archivo) formData.append("Archivo", archivo); // archivo es tipo File

    try {
      const response = await fetch("/api/Archivo/update", {
        method: "PUT",
        body: formData,
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Error: ${response.status} - ${errorText}`);
      }

      const data = await response.json();
      console.log("Archivo actualizado correctamente:", data);
      return data;
    } catch (error) {
      console.error("Error al actualizar el archivo:", error.message);
      throw error;
    }
  };

  const handleCancel = () => {
    console.log("Cancelado");
    navigate("/HomePage");
  };
  const handleSubmit = async (data, imagenes) => {
    try {
      // 1. Actualizar datos de la publicación
      const res = await fetch(
        `http://localhost:5000/api/Publicacion/byId?id=${id}`,
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(data),
        }
      );

      if (!res.ok) throw new Error("Error al guardar la publicación");

      // 2. Eliminar imágenes marcadas
      for (const imgEliminada of imagenes.imagenesEliminadas) {
        try {
          const delRes = await fetch(
            `http://localhost:5000/api/Archivo/byId?idArchivo=${imgEliminada.idArchivo}`,
            {
              method: "DELETE",
            }
          );
          if (!delRes.ok) {
            console.error(
              `Error eliminando imagen id ${imgEliminada.idArchivo}: ${delRes.statusText}`
            );
          }
        } catch (error) {
          console.error(
            `Error eliminando imagen id ${imgEliminada.idArchivo}:`,
            error
          );
        }
      }

      // 3. Subir imágenes nuevas
      for (const [i, nuevaImg] of imagenes.imagenesNuevas.entries()) {
        const formData = new FormData();
        formData.append("EntidadID", data.idPublicacion);
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

      // 4. Navegar a detalle
      setTimeout(() => {
        navigate(`/DetallePublicacion/${data.idPublicacion}`);
      }, 2000);
    } catch (error) {
      console.error(error);
      alert("No se pudo guardar la publicación");
    }
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
