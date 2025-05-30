import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom"; 
import PublicacionForm from "./FormPublicacion";
import Header from "./Header";


export default function DuplicarPublicacion() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [publicacion, setPublicacion] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:3001/publicaciones/${id}`)
      .then((res) => res.json())
      .then((data) => {
        delete data.id;
        setPublicacion(data);});
  }, [id]);

   const handleCancel = () => {
    console.log("Cancelado");
    navigate('/HomePage');
  };
  const handleSubmit = (data) => {
      const datos = {
      "id":"duplicado",/*luego se saca cunado usemos la bd */
       data
       };
     
      fetch(`http://localhost:3001/publicaciones`, {
       method: "POST",
       headers: { "Content-Type": "application/json" },
       body: JSON.stringify(datos),
      }).then(() => navigate("/HomePage"));
  };

  if (!publicacion) return <p>Cargando...</p>;

  return (
    <>
     <Header/>
     <PublicacionForm initialData={publicacion} onSubmit={handleSubmit} onCancel={handleCancel}  modo="Duplicar" />
    </>
  );
}