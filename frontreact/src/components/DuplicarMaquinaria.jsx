import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom"; 
import MaquinariaForm from "./FormMaquinaria";
import Header from "./Header";


export default function DuplicarMaquinaria() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [maquinaria, setMaquinaria] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:3001/maquinas/${id}`)
      .then((res) => res.json())
      .then((data) => {
        delete data.id;
        setMaquinaria(data);});
  }, [id]);

   const handleCancel = () => {
    console.log("Cancelado");
    navigate('/HomePage');
  };
  const handleSubmit = (data) => {
      const datos = {
      "id":data.id,/*luego se saca cunado usemos la bd */
      "marca": data.marca, 
      "modelo": data.modelo,
      "anioFabricacion": data.anio,
      "tipo": data.tipo,
      "permisosEspeciales": data.permisos.map((p)=>({"permiso":p })),//mapear al formato
      "tagsMaquina":data.tags.map((t)=>({"tag":t })),
       };
     
      fetch(`http://localhost:3001/maquinas`, {
       method: "POST",
       headers: { "Content-Type": "application/json" },
       body: JSON.stringify(datos),
      }).then(() => navigate("/"));
  };

  if (!maquinaria) return <p>Cargando...</p>;

  return (
    <>
     <Header/>
     <MaquinariaForm initialData={maquinaria} onSubmit={handleSubmit} onCancel={handleCancel}  modo="Duplicar" />
    </>
  );
}