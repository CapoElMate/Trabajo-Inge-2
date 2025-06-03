// pages/MaquinariaDetail.jsx
import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import Header from "../Header";

export default function MaquinariaDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [maquinaria, setMaquinaria] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:3001/maquinas/${id}`)
      .then((res) => res.json())
      .then((data) => setMaquinaria(data));
  }, [id]);

  const handleEliminar = () => {
    fetch(`http://localhost:3001/maquinas/${id}`, {
      method: "DELETE",
    }).then(() => navigate("/"));
    // "Maquinaria eliminada satisfactoriamente.
  };
 

  if (!maquinaria) return <p>Cargando...</p>;

  return (
    <>
    <Header/>
    <div className="p-4 detalle-contenedor">
      <h2 className="text-xl mb-2"> {maquinaria.modelo}</h2>
      <p>Año: {maquinaria.anioFabricacion}</p>
      <p>Tipo: {maquinaria.tipo}</p>
      <p>Permisos: {maquinaria.permisosEspeciales?.map(p => p.permiso.permiso).join(", ")}</p>

      <div className="flex gap-2 mt-4">
       
        <button
          style={{ // gris claro
          color: "#111827",           // casi negro
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
        onClick={() => navigate(`/modificar/${maquinaria.id}`)} >Modificar</button>
        <button 
          style={{ 
           backgroundColor: "#dd433d",
          color: "#111827",           // casi negro
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
      const confirmado = window.confirm("¿Estás seguro de que querés eliminar esta maquinaria?");
      if (confirmado) {
        handleEliminar();  
       }
        }} >Eliminar</button>
        
        <button 
          style={{  // gris claro
          backgroundColor: "#2563eb",
          color: "#111827",           // casi negro
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
        onClick={() => navigate(`/duplicar/${maquinaria.id}`)}  >Duplicar</button>
      </div>
    </div>
    </>
  );
}
