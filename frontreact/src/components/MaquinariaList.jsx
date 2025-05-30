// pages/MaquinariaList.jsx
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Header from "./Header";

export default function MaquinariaList() {
  const [maquinarias, setMaquinarias] = useState([]);

  useEffect(() => {
    fetch("http://localhost:3001/maquinas")
      .then((res) => res.json())
      .then((data) => setMaquinarias(data));
  }, []);

  return (
    <>
    <Header/>
    <div className="p-4">
      <h2 className="text-xl mb-4">Listado de Maquinarias Provisorio</h2>
      <ul className="space-y-2">
        {maquinarias.map((maq) => (
          <li key={maq.id} className="border p-2 rounded shadow">
            <div className="flex justify-between items-center">
              <span>{maq.nombre} - {maq.modelo}</span>
              <Link to={`/maquinaria/${maq.id}`} className="text-blue-500 underline">
                Ver detalle
              </Link>
            </div>
          </li>
        ))}
      </ul>
    </div>
    </>
  );
}
