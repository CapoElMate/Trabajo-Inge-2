import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import CargarMaquinaria from "../components/CargarMaquinaria"; // formulario reutilizable
import MaquinariaForm from "./FormMaquinaria";
import Header2 from "./Header2";


export default function EditarMaquinaria() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [maquinaria, setMaquinaria] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:3001/maquinas/${id}`)
      .then((res) => res.json())
      .then((data) => setMaquinaria(data));
  }, [id]);

   const handleCancel = () => {
    console.log("Cancelado");
    navigate('/HomePage');
  };
  const handleSubmit = (datos) => {
    fetch(`http://localhost:3001/maquinas/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(datos),
    }).then(() => navigate("/"));
  };

  if (!maquinaria) return <p>Cargando...</p>;

  return (
    <>
     <Header2/>
     <MaquinariaForm initialData={maquinaria} onSubmit={handleSubmit} onCancel={handleCancel}  modo="Editar" />
    </>
  );
}