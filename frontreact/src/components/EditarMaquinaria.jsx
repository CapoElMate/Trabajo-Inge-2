import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import CargarMaquinaria from "../components/CargarMaquinaria"; // formulario reutilizable
import MaquinariaForm from "./FormMaquinaria";
import Header from "./Header";


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
  const handleSubmit = (data) => {
      const datos = {
      "id":data.id,
      "marca": data.marca, 
      "modelo": data.modelo,
      "anioFabricacion": data.anio,
      "tipo": data.tipo,
      "permisosEspeciales": data.permisos.map((p)=>({"permiso":p })),//mapear al formato
      "tagsMaquina":data.tags.map((t)=>({"tag":t })),
       };
    fetch(`http://localhost:3001/maquinas/${id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(datos),
    }).then(() => navigate("/"));
  };

  if (!maquinaria) return <p>Cargando...</p>;

  return (
    <>
     <Header/>
     <MaquinariaForm initialData={maquinaria} onSubmit={handleSubmit} onCancel={handleCancel}  modo="Editar" />
    </>
  );
}