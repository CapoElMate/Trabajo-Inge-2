import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom"; 
import PublicacionForm from "./FormPublicacion";
import Header from "./Header";


export default function EditarPublicacion() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [publicacion, setPublicacion] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:3001/publicaciones/${id}`)
      .then((res) => res.json())
      .then((data) => setPublicacion(data));
  }, [id]);

   const handleCancel = () => {
    console.log("Cancelado");
    navigate('/HomePage');
  };
  const handleSubmit = (data) => { 
     //como ya esta formateado en el form publicaciones no lo formateo aca para mandaral back
      fetch( `http://localhost:3001/publicaciones/${id}`, {
       method: "PUT",
       headers: { "Content-Type": "application/json" },
       body: JSON.stringify(data),
      }).then(() => navigate("/HomePage"));
  };

  if (!publicacion) return <p>Cargando...</p>;

  return (
    <>
     <Header/>
     <PublicacionForm initialData={publicacion} onSubmit={handleSubmit} onCancel={handleCancel}  modo="Editar" />
    </>
  );
}