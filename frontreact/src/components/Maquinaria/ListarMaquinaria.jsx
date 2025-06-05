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
  const [marcas, setMarcas] = useState([]);
  const [modelos, setModelos] = useState([]);
  const [tipos, setTipos] = useState([]);
  const [mensaje, setMensaje] = useState("");

  const [filtros, setFiltros] = useState({
    marca: "",
    modelo: "",
    tipoMaquina: "",
    anioDesde: "",
    anioHasta: "",
    tag: ""
  });

  const navigate = useNavigate();

  useEffect(() => {
    fetch("http://localhost:5000/api/Maquinas/all")
      .then((res) => res.json())
      .then((data) => {
        setMaquinarias(data);
        const tiposUnicos = [...new Set(data.map((m) => m.tipoMaquina.tipo))];
        setTipos(tiposUnicos);
      });

    getMarcas().then(setMarcas);
  }, [refresh]);

  useEffect(() => {
    if (filtros.marca) {
      getModelos(filtros.marca).then(setModelos);
    } else {
      setModelos([]);
    }
  }, [filtros.marca]);

  const getMarcas = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Marcas/all");
      if (!response.ok) throw new Error("Error al obtener las marcas");
      const data = await response.json();
      return data.map((m) => m.marca);
    } catch (error) {
      console.error("Error fetching marcas:", error);
      return [];
    }
  };

  const getModelos = async (marca = "") => {
    if (!marca) return [];
    try {
      const response = await fetch("http://localhost:5000/api/Modelos/all");
      if (!response.ok) throw new Error("Error al obtener los modelos");
      const data = await response.json();
      return data.filter((m) => m.marca.marca === marca).map((m) => m.modelo);
    } catch (error) {
      console.error("Error fetching modelos:", error);
      return [];
    }
  };

  const handleEliminar = async (idMaquina) => {
    try {
      const res = await fetch(
        `http://localhost:5000/api/Maquinas/byId/logic?id=${idMaquina}`,
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

      const duplicarJson = await createRes.json();

      if (!createRes.ok) throw new Error("Error al duplicar la máquina");

      navigate(`/ModificarMaquinaria/${duplicarJson.idMaquina}`);
      setRefresh(!refresh);
    } catch (error) {
      console.error(error);
      alert("No se pudo duplicar la máquina");
    }
  };

  const handleFiltrar = () => {
    const { marca, modelo, tipoMaquina, anioDesde, anioHasta, tag } = filtros;

    if (anioDesde && anioHasta && parseInt(anioDesde) > parseInt(anioHasta)) {
      setMensaje('El año "desde" no puede ser mayor que el año "hasta".');
      return;
    }

    setMensaje("");

    fetch("http://localhost:5000/api/Maquinas/all")
      .then((res) => res.json())
      .then((data) => {
        const filtradas = data.filter((maq) => {
          return (
            (!marca || maq.modelo.marca.marca === marca) &&
            (!modelo || maq.modelo.modelo === modelo) &&
            (!tipoMaquina || maq.tipoMaquina.tipo === tipoMaquina) &&
            (!anioDesde || maq.anioFabricacion >= parseInt(anioDesde)) &&
            (!anioHasta || maq.anioFabricacion <= parseInt(anioHasta)) &&
            (!tag || maq.tagsMaquina.some(t => t.tag.toLowerCase().includes(tag.toLowerCase())))
          );
        });

        if (filtradas.length === 0) {
          setMensaje("No se encontraron maquinarias con esas características");
        }

        setMaquinarias(filtradas);
      });
  };

  return (
    <>
      <Header />
      <div className="container" style={{ display: "flex", gap: "2rem" }}>
        {/* Filtros */}
        <div style={{ width: "300px", display: "flex", flexDirection: "column", gap: "0.5rem" }}>
          <select
            value={filtros.marca}
            onChange={(e) => setFiltros({ ...filtros, marca: e.target.value, modelo: "" })}
          >
            <option value="">Seleccionar marca</option>
            {marcas.map((m, i) => (
              <option key={i} value={m}>{m}</option>
            ))}
          </select>

          <select
            value={filtros.modelo}
            onChange={(e) => setFiltros({ ...filtros, modelo: e.target.value })}
            disabled={!filtros.marca}
          >
            <option value="">Seleccionar modelo</option>
            {modelos.map((m, i) => (
              <option key={i} value={m}>{m}</option>
            ))}
          </select>

          <select
            value={filtros.tipoMaquina}
            onChange={(e) => setFiltros({ ...filtros, tipoMaquina: e.target.value })}
          >
            <option value="">Seleccionar tipo</option>
            {tipos.map((t, i) => (
              <option key={i} value={t}>{t}</option>
            ))}
          </select>

          <select
            value={filtros.anioDesde}
            onChange={(e) => setFiltros({ ...filtros, anioDesde: e.target.value })}
          >
            <option value="">Año desde</option>
            {Array.from({ length: 50 }, (_, i) => {
              const year = new Date().getFullYear() - i;
              return <option key={year} value={year}>{year}</option>;
            })}
          </select>

          <select
            value={filtros.anioHasta}
            onChange={(e) => setFiltros({ ...filtros, anioHasta: e.target.value })}
          >
            <option value="">Año hasta</option>
            {Array.from({ length: 50 }, (_, i) => {
              const year = new Date().getFullYear() - i;
              return <option key={year} value={year}>{year}</option>;
            })}
          </select>

          <input
            type="text"
            placeholder="Tag"
            value={filtros.tag}
            onChange={(e) => setFiltros({ ...filtros, tag: e.target.value })}
          />
          <StyledButton text="Filtrar" onClick={handleFiltrar} />

          {mensaje && <div style={{ color: "red" }}>{mensaje}</div>}
        </div>

        {/* Lista de maquinarias */}
        <div className="card_container" style={{ flex: 1 }}>
          {maquinarias.map((maq) => (
            <div key={maq.idMaquina} className="card">
              <div className="card_header">
                <h3>ID: {maq.idMaquina}</h3>
                <div className="title">
                  <h3>{maq.modelo.marca.marca}</h3>
                  <h3>{maq.modelo.modelo}</h3>
                </div>
              </div>
      <div className="container">
        <div className="card_container">
  {maquinarias.filter(m => m.status !== "Eliminada").map((maq) => (
    <div key={maq.idMaquina} className="card">
      <div className="card_header">
        <h3>ID: {maq.idMaquina}</h3>
        <div className="title">
          <h3>{maq.modelo.marca.marca}</h3>
          <h3>{maq.modelo.modelo}</h3>
        </div>
      </div>

              <div style={{ flex: 1 }}>
                <div className="card_body">
                  <p>Año de fabricación: {maq.anioFabricacion}</p>
                  <p>Tipo de máquina: {maq.tipoMaquina.tipo}</p>
                </div>

                <div className="mt-2">
                  <p className="tagTitle">Tags</p>
                  <div className="tags-container">
                    {maq.tagsMaquina?.length > 0 ? (
                      maq.tagsMaquina.map((tag, index) => (
                        <span key={index} className="tag">{tag.tag}</span>
                      ))
                    ) : (
                      <span className="tag">Ninguno</span>
                    )}
                  </div>
                </div>

                <div className="mt-2">
                  <p className="tagTitle">Permisos especiales</p>
                  <div className="permisos-container">
                    {maq.permisosEspeciales?.length > 0 ? (
                      maq.permisosEspeciales.map((perm, index) => (
                        <span key={index} className="permiso">{perm.permiso}</span>
                      ))
                    ) : (
                      <span className="permiso">Ninguno</span>
                    )}
                  </div>
                </div>
              </div>

              <div className="button-container">
                <StyledButton text="Modificar" onClick={() => navigate(`/ModificarMaquinaria/${maq.idMaquina}`)} />
                <StyledButton text="Eliminar" onClick={() => {
                  setMaquinaAEliminar(maq.idMaquina);
                  setMostrarModal(true);
                }} />
                <StyledButton text="Duplicar" onClick={() => handleDuplicar(maq.idMaquina)} />
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
