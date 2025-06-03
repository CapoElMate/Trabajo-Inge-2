// pages/MaquinariaList.jsx
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Header from "../Header";
import StyledButton from "../CustomButton";
import ConfirmModal from "../Modal";
import "./ListarMaquinaria.css";

export default function MaquinariaList() {
  const [maquinarias, setMaquinarias] = useState([]);
  const [refresh, setRefresh] = useState(false);
  const [mostrarModal, setMostrarModal] = useState(false);
  const [maquinaAEliminar, setMaquinaAEliminar] = useState(null);
  const navigate = useNavigate();

  const handleEliminar = async (idMaquina) => {
    try {
      const res = await fetch(
        `http://localhost:5000/api/Maquinas/byId?id=${idMaquina}`,
        { method: "DELETE" }
      );
      if (!res.ok) throw new Error("Error al eliminar la máquina");
      setRefresh(!refresh);
    } catch (error) {
      console.error(error);
      alert("No se pudo eliminar la máquina");
    }
  };

  const handleDuplicar = async (idMaquina) => {
    try {
      const res = await fetch(
        `http://localhost:5000/api/Maquinas/byId?id=${idMaquina}`
      );
      if (!res.ok) throw new Error("Error al obtener la máquina para duplicar");
      const maquinaOriginal = await res.json();

      const maquinaDuplicada = {
        ...maquinaOriginal,
        idMaquina: undefined,
        status: "Activa",
      };

      const createRes = await fetch("http://localhost:5000/api/Maquinas", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(maquinaDuplicada),
      });

      let duplicarJson = await createRes.json();

      navigate(`/ModificarMaquinaria/${duplicarJson.idMaquina}`);
      if (!createRes.ok) throw new Error("Error al duplicar la máquina");
      setRefresh(!refresh);
    } catch (error) {
      console.error(error);
      alert("No se pudo duplicar la máquina");
    }
  };

  useEffect(() => {
    fetch("http://localhost:5000/api/Maquinas/all")
      .then((res) => res.json())
      .then((data) => setMaquinarias(data));
  }, [refresh]);

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
                <p>Año de fabricación: {maq.anioFabricacion}</p>
                <p>Tipo de máquina: {maq.tipoMaquina.tipo}</p>
              </div>

              <div className="mt-2">
                <p className="text-sm font-medium text-gray-600 tagTitle">Tags</p>
                <div className="flex flex-wrap gap-2 mt-1">
                  {maq.tagsMaquina?.length > 0 ? (
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
                <p className="text-sm font-medium text-gray-600 tagTitle">Permisos especiales</p>
                <div className="flex flex-wrap gap-2 mt-1">
                  {maq.permisosEspeciales?.length > 0 ? (
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

              <div className="button-container">
                <StyledButton
                  text="Modificar"
                  onClick={() => navigate(`/ModificarMaquinaria/${maq.idMaquina}`)}
                />
                <StyledButton
                  text="Eliminar"
                  onClick={() => {
                    setMaquinaAEliminar(maq.idMaquina);
                    setMostrarModal(true);
                  }}
                />
                <StyledButton
                  text="Duplicar"
                  onClick={() => handleDuplicar(maq.idMaquina)}
                />
              </div>
            </div>
          ))}
        </div>
      </div>

      <ConfirmModal
        isOpen={mostrarModal}
        onClose={() => setMostrarModal(false)}
        onConfirm={() => {
          handleEliminar(maquinaAEliminar);
          setMostrarModal(false);
        }}
        mensaje="¿Estás seguro de que querés eliminar esta máquina?"
      />
    </>
  );
}