// pages/MaquinariaList.jsx
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Header from "./Header";
import "./MaquinariaList.css";

export default function MaquinariaList() {
  const [maquinarias, setMaquinarias] = useState([]);

  useEffect(() => {
    fetch("http://localhost:5000/api/Maquinas/all")
      .then((res) => res.json())
      .then((data) => setMaquinarias(data));
  }, []);

  return (
    <>
      <Header />
      <div className="container">
        <div className="card_container">
          {maquinarias.map((maq) => (
            <div key={maq.idMaquina} className="card">
              <div className="card_header">
                <h3>ID: {maq.idMaquina}</h3>
                <div className="title">
                  <h3>{maq.modelo.marca.marca}</h3>
                  <h3>{maq.modelo.modelo}</h3>
                </div>
              </div>
              <div className="card_body">
                <p>Año de fabricacion: {maq.anioFabricacion}</p>
                <p>Tipo de maquina: {maq.tipoMaquina.tipo}</p>
              </div>
              <div className="mt-2">
                <p className="text-sm font-medium text-gray-600 tagTitle">
                  Tags
                </p>
                <div className="flex flex-wrap gap-2 mt-1">
                  {maq.tagsMaquina && maq.tagsMaquina.length > 0 ? (
                    maq.tagsMaquina.map((tag, index) => (
                      <span
                        key={index}
                        className="bg-blue-100 text-blue-700 text-xs font-medium px-3 py-0.5 rounded-full"
                      >
                        {tag.tag}
                      </span>
                    ))
                  ) : (
                    <span className="bg-gray-200 text-gray-700 text-xs font-medium px-3 py-0.5 rounded-full">
                      N/A
                    </span>
                  )}
                </div>
              </div>

              <div className="mt-2">
                <p className="text-sm font-medium text-gray-600 tagTitle">
                  Permisos especiales
                </p>
                <div className="flex flex-wrap gap-2 mt-1">
                  {maq.permisosEspeciales &&
                  maq.permisosEspeciales.length > 0 ? (
                    maq.permisosEspeciales.map((perm, index) => (
                      <span
                        key={index}
                        className="bg-green-100 text-green-700 text-xs font-medium px-3 py-0.5 rounded-full"
                      >
                        {perm.permiso}
                      </span>
                    ))
                  ) : (
                    <span className="bg-gray-200 text-gray-700 text-xs font-medium px-3 py-0.5 rounded-full">
                      N/A
                    </span>
                  )}
                </div>
              </div>
              <button
                className="px-4 py-2 rounded text-white buttonStyle"
              >
                Duplicar
              </button>
            </div>
          ))}
        </div>
      </div>
    </>
  );
}
