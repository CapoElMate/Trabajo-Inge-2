// pages/MaquinariaDetail.jsx
import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";

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
  };

  const handleDuplicar = () => {
    const copia = { ...maquinaria };
    delete copia.id;
    fetch(`http://localhost:3001/maquinas`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(copia),
    }).then(() => navigate("/"));
  };

  if (!maquinaria) return <p>Cargando...</p>;

  return (
    <div className="p-4 detalle-contenedor">
      <h2 className="text-xl mb-2"> {maquinaria.modelo}</h2>
      <p>Año: {maquinaria.anioFabricacion}</p>
      <p>Tipo: {maquinaria.tipo}</p>
      <p>Permisos: {maquinaria.permisosEspeciales?.map(p => p.permiso).join(", ")}</p>

      <div className="flex gap-2 mt-4">
        <button onClick={handleEliminar} className="bg-red-500 text-white px-2 py-1 rounded">Eliminar</button>
        <button onClick={() => navigate(`/modificar/${maquinaria.id}`)} className="bg-yellow-500 text-white px-2 py-1 rounded">Modificar</button>
        <button onClick={handleDuplicar} className="bg-blue-500 text-white px-2 py-1 rounded">Duplicar</button>
      </div>
    </div>
  );
}
