// pages/MaquinariaDetail.jsx
import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import Header from "../Header";
import StyledButton from "../CustomButton";
import "./DetallePublicacion.css";
import logo from "../../assets/bobElAlquiladorLogoCompleto.svg";
import ConfirmModal from "../Modal";
import ModalReserva from "../Reserva/ModalReserva";

export default function PublicacionDetail() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [publicacion, setPublicacion] = useState(null);
  const [publicacionEliminar, setPublicacionEliminar] = useState(null);
  const [mostrarModal, setMostrarModal] = useState(false);
  const [mostrarReservaModal, setMostrarReservaModal] = useState(false);

  useEffect(() => {
    fetch(`http://localhost:5000/api/Publicacion/byId?id=${id}`)
      .then((res) => res.json())
      .then((data) => setPublicacion(data));
  }, [id]);

  const handleReservar = (state) => {
    if(state === "success" || state === "error")
      navigate("/HomePage");
  };

  // const handlePago = () =>
  // {
  //   setMostrarPagoModal(false);
  // }

  const handleEliminar = async (idPublicacion) => {
    try {
      const res = await fetch(
        `http://localhost:5000/api/Publicacion/byId?id=${idPublicacion}`,
        { method: "DELETE" }
      );
      if (!res.ok) throw new Error("Error al eliminar la publicacion");
      navigate("/HomePage");
    } catch (error) {
      console.error(error);
      alert("No se pudo eliminar la publicacion");
    }
  };

  const handleDuplicar = async (idPublicacion) => {
    try {
      const res = await fetch(
        `http://localhost:5000/api/Publicacion/byId?id=${idPublicacion}`
      );
      if (!res.ok)
        throw new Error("Error al obtener la publicacion para duplicar");
      const publicacionOriginal = await res.json();

      const maquinaDuplicada = {
        ...publicacionOriginal,
        id: idPublicacion,
        status: "Activa",
      };

      const createRes = await fetch("http://localhost:5000/api/Publicacion", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(maquinaDuplicada),
      });

      let duplicarJson = await createRes.json();

      navigate(`/ModificarPublicacion/${duplicarJson.idPublicacion}`);
      if (!createRes.ok) throw new Error("Error al duplicar la publicacion");
    } catch (error) {
      console.error(error);
      alert("No se pudo duplicar la publicacion");
    }
  };

  if (!publicacion) return <p>Cargando...</p>;

  return (
    <>
      <Header />
      <div className="p-4 detalle-contenedor">
        <div className="header">
          <h2>{publicacion.titulo}</h2>
          <div className="button-container">
            <StyledButton
              text="Modificar"
              onClick={() =>
                navigate(`/EditarPublicacion/${publicacion.idPublicacion}`)
              }
            />
            <StyledButton
              text="Eliminar"
              onClick={() => {
                setPublicacionEliminar(publicacion.idPublicacion);
                setMostrarModal(true);
              }}
            />
            <StyledButton
              text="Duplicar"
              onClick={() => {
                handleDuplicar(publicacion.idPublicacion);
              }}
            />
            <StyledButton
              text="Reservar"
              onClick={() => setMostrarReservaModal(true)}
            />
          </div>
        </div>

        {/* Nueva división superior */}
        <div className="top-content">
          {/* Izquierda: info publicación */}
          <div className="container">
            <h3>Descripción</h3>
            <p id="descripcion">{publicacion.descripcion}</p>

            <h3>Ubicación</h3>
            <p>{publicacion.ubicacion.ubicacionName}</p>

            <h3>Política de Cancelación</h3>
            <p>{publicacion.politicaDeCancelacion.politica}</p>

            <h3>Precio por Día</h3>
            <p>{publicacion.precioPorDia}</p>

            <h3>Tags</h3>
            <p>{publicacion.tagsPublicacion.map((t) => t.tag).join(", ")}</p>
          </div>

          {/* Derecha: imágenes */}
          <div className="image-preview">
            {publicacion.imagenes && publicacion.imagenes.length > 0 ? (
              publicacion.imagenes.map((i) => (
                <img
                  key={i.img.name}
                  src={i.img.src}
                  width="120px"
                  height="120px"
                  alt="preview"
                />
              ))
            ) : (
              <img src={logo} alt="Vista previa" className="preview-img" />
            )}
          </div>
        </div>

        {/* Abajo: maquinaria */}
        <h3 className="maquinaria-title">Maquinaria</h3>
        <div className="maquinaria-container">
          {/* Fila 1 */}
          <div className="maquinaria-row">
            <div className="maquinaria-item">
              <h4>ID</h4>
              <p>{publicacion.maquina.idMaquina}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Estado</h4>
              <p>{publicacion.maquina.status}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Año de Fabricación</h4>
              <p>{publicacion.maquina.anioFabricacion}</p>
            </div>
          </div>

          {/* Fila 2 */}
          <div className="maquinaria-row">
            <div className="maquinaria-item">
              <h4>Modelo</h4>
              <p>{publicacion.maquina.modelo.modelo}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Marca</h4>
              <p>{publicacion.maquina.modelo.marca.marca}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Tipo</h4>
              <p>{publicacion.maquina.tipoMaquina.tipo}</p>
            </div>
          </div>

          {/* Fila 3 */}
          <div className="maquinaria-row">
            <div className="maquinaria-item">
              <h4>Tags</h4>
              <p>
                {publicacion.maquina.tagsMaquina.map((t) => t.tag).join(", ")}
              </p>
            </div>
            <div className="maquinaria-item">
              <h4>Permisos Especiales</h4>
              <p>
                {publicacion.maquina.permisosEspeciales.length === 0
                  ? "N/A"
                  : publicacion.maquina.permisosEspeciales
                      .map((p) => p.permiso)
                      .join(", ")}
              </p>
            </div>
            <div className="maquinaria-item"></div>
          </div>
        </div>
      </div>
      <ConfirmModal
        isOpen={mostrarModal}
        onClose={() => setMostrarModal(false)}
        onConfirm={() => {
          handleEliminar(publicacionEliminar);
          setMostrarModal(false);
        }}
        mensaje="¿Estás seguro de que querés eliminar esta publicacion?"
      />
      <ModalReserva
        idPublicacion={publicacion.idPublicacion}
        precioPorDia={publicacion.precioPorDia}
        isOpen={mostrarReservaModal}
        onClose={() => setMostrarReservaModal(false)}
        onReservar={handleReservar}
      />
      {/* <ModalPago
        isOpen={mostrarPagoModal}
        onClose={() => setMostrarPagoModal(false)}
        onPago={handlePago}
      /> */}
    </>
  );
}
