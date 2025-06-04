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
  const [errorVerificacion, setErrorVerificacion] = useState(false);
  const [noRenderizar, setNoRenderizar] = useState(false);
  const [exito, setExito] = useState(false);
  const [imagenes, setImagenes] = useState([]);
  const [currentImageIndex, setCurrentImageIndex] = useState(0);
  useEffect(() => {
    fetch(`http://localhost:5000/api/Publicacion/byId?id=${id}`)
      .then((res) => res.json())
      .then((data) => {
        if (data.status === "Eliminada") {
          setNoRenderizar(true);
        }
        setPublicacion(data);

        // Solo cargar imágenes si la publicación no está eliminada
        if (data.status !== "Eliminada") {
          return fetch(
            `http://localhost:5000/api/Archivo/byEntidad?entidadId=${data.idPublicacion}&tipoEntidad=0`
          );
        } else {
          return null;
        }
      })
      .then((res) => {
        if (res) return res.json();
      })
      .then((imagenesData) => {
        if (imagenesData) {
          setImagenes(imagenesData);
        }
      })
      .catch((error) => {
        console.error("Error al cargar la publicación o imágenes:", error);
      });
  }, [id]);

  const handlePrev = () => {
    setCurrentImageIndex((prev) =>
      prev === 0 ? imagenes.length - 1 : prev - 1
    );
  };

  const handleNext = () => {
    setCurrentImageIndex((prev) =>
      prev === imagenes.length - 1 ? 0 : prev + 1
    );
  };

  const handleReservar = (state) => {
    if (state === "success" || state === "error") {
      publicacion.status = "No disponible";
      fetch(
        `http://localhost:5000/api/Publicacion/byId?id=${publicacion.idPublicacion}`,
        {
          method: "PUT",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(publicacion),
        }
      )
        .then(() => {
          const maquinariaActualizada = {
            ...publicacion.maquina,
            status: "No disponible",
          };
          fetch(
            `http://localhost:5000/api/Maquinas/byId?id=${publicacion.maquina.idMaquina}`,
            {
              method: "PUT",
              headers: { "Content-Type": "application/json" },
              body: JSON.stringify(maquinariaActualizada),
            }
          );
          navigate("/HomePage");
        })
        .catch((error) => {
          console.error("Error al actualizar la publicación:", error);
        });
    }
  };

  // const handlePago = () =>
  // {
  //   setMostrarPagoModal(false);
  // }

  const verificarReservas = async (idPublicacion) => {
    try {
      const reservas = await fetch(`http://localhost:5000/api/Reserva/all`);
      const reservasData = await reservas.json();

      const reservasPublicacion = reservasData.filter(
        (reserva) =>
          reserva.idPublicacion === idPublicacion &&
          reserva.status === "Lista para efectivizar"
      );

      if (reservasPublicacion.length > 0) {
        console.log(
          "No se puede eliminar la publicacion porque tiene reservas asociadas"
        );
        return true;
      } else return false;
    } catch (error) {
      console.error("Error al verificar reservas:", error);
    }
  };

  const verificarAlquileres = async (idPublicacion) => {
    try {
      const alquileres = await fetch(`http://localhost:5000/api/Alquiler/all`);
      const alquileresData = await alquileres.json();

      const alquileresPublicacion = alquileresData.filter(
        (alquiler) =>
          alquiler.reserva.idPublicacion === idPublicacion &&
          alquiler.status === "Efectivizado"
      );

      if (alquileresPublicacion.length > 0) {
        console.log(
          "No se puede eliminar la publicacion porque tiene alquileres asociados"
        );
        return true;
      } else return false;
    } catch (error) {
      console.error("Error al verificar alquileres:", error);
    }
  };

  const handleEliminar = async (idPublicacion) => {
    try {
      let verificacion = false;
      verificacion = await verificarReservas(idPublicacion);
      !verificacion
        ? (verificacion = await verificarAlquileres(idPublicacion))
        : verificacion;

      if (verificacion === true) {
        setErrorVerificacion(true);
        setTimeout(() => {
          setErrorVerificacion(false);
          setMostrarModal(false);
        }, 1250);
        return;
      }

      const res = await fetch(
        `http://localhost:5000/api/Publicacion/byId/logic?id=${idPublicacion}`,
        { method: "DELETE" }
      );
      if (!res.ok) throw new Error("Error al eliminar la publicacion");

      setExito(true);
      setTimeout(() => {
        setExito(false);
        setMostrarModal(false);
        navigate("/HomePage");
      }, 1250);
    } catch (error) {
      console.error(error);
      console.log("No se pudo eliminar la publicacion");
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

      const publicacionDuplicada = {
        ...publicacionOriginal,
        id: undefined,
        status: "Disponible",
      };

      const createRes = await fetch("http://localhost:5000/api/Publicacion", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(publicacionDuplicada),
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

      {noRenderizar ? (
        <div className="p-4 detalle-contenedor">
          <h2>Publicación no encontrada</h2>
        </div>
      ) : (
        <>
          <div className="p-4 detalle-contenedor">
            <div className="header">
              <h2>{publicacion.titulo}</h2>
              <div className="button-container">
                <StyledButton
                  text="Modificar"
                  onClick={() =>
                    navigate(`/ModificarPublicacion/${publicacion.idPublicacion}`)
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
                <p>
                  {publicacion.tagsPublicacion.map((t) => t.tag).join(", ")}
                </p>
              </div>

              <div className="image-preview">
                {imagenes && imagenes.length > 0 ? (
                  <div className="carousel">
                    <img
                      src={`data:image/jpeg;base64,${imagenes[currentImageIndex].archivoBase64}`}
                      alt={`Imagen ${currentImageIndex + 1}`}
                      width="300px"
                      height="300px"
                      className="carousel-image"
                    />
                    {imagenes.length > 1 && (
                      <div className="carousel-controls">
                        <button onClick={handlePrev}>⟨</button>
                        <span>
                          {currentImageIndex + 1} /{" "}
                          {imagenes.length}
                        </span>
                        <button onClick={handleNext}>⟩</button>
                      </div>
                    )}
                  </div>
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
                    {publicacion.maquina.tagsMaquina.length === 0
                      ? "N/A"
                      :
                    publicacion.maquina.tagsMaquina
                      .map((t) => t.tag)
                      .join(", ")}
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
              </div>
            </div>
          </div>

          <ConfirmModal
            isOpen={mostrarModal}
            onClose={() => setMostrarModal(false)}
            onConfirm={() => {
              handleEliminar(publicacionEliminar);
            }}
            mensaje="¿Estás seguro de que querés eliminar esta publicacion?"
            mensajeError={
              errorVerificacion
                ? "No se puede eliminar esta publicación. Tiene alquileres o reservas activas."
                : ""
            }
            mensajeExito={exito ? "Se eliminó la publicación exitosamente" : ""}
          />

          <ModalReserva
            idPublicacion={publicacion.idPublicacion}
            precioPorDia={publicacion.precioPorDia}
            isOpen={mostrarReservaModal}
            onClose={() => setMostrarReservaModal(false)}
            onReservar={handleReservar}
          />

          {/* 
          <ModalPago
            isOpen={mostrarPagoModal}
            onClose={() => setMostrarPagoModal(false)}
            onPago={handlePago}
          /> 
          */}
        </>
      )}
    </>
  );
}
