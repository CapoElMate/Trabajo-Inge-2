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
  const handleSubmit = async (data) => {
    //como ya esta formateado en el form publicaciones no lo formateo aca para mandaral back
    fetch(`http://localhost:5000/Publicacion/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(data),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al guardar la maquinaria");
        const resJson = res.json();
        navigate(`/DetailsPublicacion/${resJson.idPublicacion}`);
      })
      .catch((error) => {
        console.error(error);
        alert("No se pudo guardar la maquinaria");
      });
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
