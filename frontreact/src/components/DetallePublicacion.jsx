 // pages/MaquinariaDetail.jsx
import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import Header from "./Header";
import DetalleMaquinariaSimple from "./DetalleMaquinariaSimple";

export default function PublicacionDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [publicacion, setPublicacion] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:3001/publicaciones/${id}`)
      .then((res) => res.json())
      .then((data) => setPublicacion(data));
  }, [id]);

  const handleEliminar = () => {
    fetch(`http://localhost:3001/publicaciones/${id}`, {
      method: "DELETE",
    }).then(() => navigate("/"));
    // "publicacion eliminada satisfactoriamente.
  };
 

  if (!publicacion) return <p>Cargando...</p>;

  return (
    <>
    <Header/>
    <div className="p-4 detalle-contenedor">
      <h2 className="text-xl mb-2"> {publicacion.titulo}</h2>
      <p>Descripcion: {publicacion.descripcion}</p>
      <p>Ubicacion: {publicacion.ubicacion}</p>
      <p>Politica: {publicacion.politica}</p>
      <p>Precio: {publicacion.precio}</p>
      <p>Tags: {publicacion.tags?.map(t => t.tag).join(", ")}</p>
      <p>Imagenes:</p>
       {publicacion.imagenes.map((i)=>(
        <img key={i.img.name} src={i.img.src} width="120px" height="120px" />  )

       )}
      <p>Maquinaria: </p>
      <>
        <DetalleMaquinariaSimple maquinaria={publicacion.maquinaria} />
      </> 
     
       
     
        
        
      <div className="flex gap-2 mt-4">
       
        <button
          style={{ 
          color: "#111827",           
          padding: "10px 16px",
          border: "1px solid #d1d5db",
          borderRadius: "8px",
          cursor: "pointer",
          fontSize: "16px",
          fontWeight: "500",
          boxShadow: "0 1px 3px rgba(0,0,0,0.05)",
          transition: "background-color 0.2s ease"
        }}
        onMouseOver={(e) => (e.target.style.backgroundColor = "#d1d5db")}
        onMouseOut={(e) => (e.target.style.backgroundColor = "#e5e7eb")} 
        onClick={() => navigate(`/EditarPublicacion/${publicacion.id}`)} >Modificar</button>
        <button 
          style={{ 
           backgroundColor: "#dd433d",
          color: "#111827",          
          padding: "10px 16px",
          border: "1px solid #d1d5db",
          borderRadius: "8px",
          cursor: "pointer",
          fontSize: "16px",
          fontWeight: "500",
          boxShadow: "0 1px 3px rgba(0,0,0,0.05)",
          transition: "background-color 0.2s ease"
        }}
        onMouseOver={(e) => (e.target.style.backgroundColor = "#C8797D")}
        onMouseOut={(e) => (e.target.style.backgroundColor = "#dd433d")}
        onClick={() => {
      const confirmado = window.confirm("¿Estás seguro de que querés eliminar esta publicacion?");
      if (confirmado) {
        handleEliminar();  
       }
        }} >Eliminar</button>
        
        <button 
          style={{   
          backgroundColor: "#2563eb",
          color: "#111827",            
          padding: "10px 16px",
          border: "1px solid #d1d5db",
          borderRadius: "8px",
          cursor: "pointer",
          fontSize: "16px",
          fontWeight: "500",
          boxShadow: "0 1px 3px rgba(0,0,0,0.05)",
          transition: "background-color 0.2s ease"
        }}
        onMouseOver={(e) => (e.target.style.backgroundColor = "#1e40af")}
        onMouseOut={(e) => (e.target.style.backgroundColor = "#2563eb")}
        onClick={() => navigate(`/DuplicarPublicacion/${publicacion.id}`)}  >Duplicar</button>
      </div>
    </div>
    </>
  );
}
