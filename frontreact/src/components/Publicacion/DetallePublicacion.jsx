// pages/MaquinariaDetail.jsx
import { useParams, useNavigate, useLocation } from "react-router-dom";
import { useEffect, useState } from "react";
import Header from "../Header";
import StyledButton from "../CustomButton";
import "./DetallePublicacion.css";
import logo from "../../assets/bobElAlquiladorLogoCompleto.svg";
import ConfirmModal from "../Modal";
import ModalReserva from "../Reserva/ModalReserva";
import { useAuth } from "../../AuthContext";
import ModalResultado from "./ModalResultado";

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
  const { user, loadAuth } = useAuth();
  const location = useLocation();
  const [mostrarRtdoModal, setMostrarRtdoModal] = useState(false);
  const [colorRtdo, setColorRtdo] = useState("#dc3545");
  const [rtdo, setRtdo] = useState("rtdo");
  const [state, setState] = useState(location.state);

  useEffect(() => {
    const params = new URLSearchParams(location.search);
    setState(params.get("state"));

    if (state === "success") {
      const reserva = {
        fecInicio: new Date(params.get("fecInicio")).toISOString(),
        fecFin: new Date(params.get("fecFin")).toISOString(),
        status: params.get("status"),
        calle: params.get("calle"),
        altura: params.get("altura"),
        dpto: params.get("dpto"),
        piso: params.get("piso"),
        tipoEntrega: {
          entrega: params.get("entrega"),
        },
        idAlquiler:
          params.get("idAlquiler") === "null" ? null : params.get("idAlquiler"),
        dniCliente: params.get("dniCliente"),
        idPublicacion: Number(params.get("idPublicacion")),
        montoTotal: parseFloat(params.get("montoTotal")),
      };

      fetch("http://localhost:5000/api/Reserva", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(reserva),
      })
        .then((response) => {
          if (!response.ok) {
            throw new Error("Error al crear la reserva");
          }
          return response.json();
        })
        .then((data) => {
          setMostrarRtdoModal(true);
          setTimeout(() => {
            setMostrarRtdoModal(false);
          }, 1500);

          console.log("Reserva creada con éxito:", data);
          setRtdo("La reserva se creo satisfactoriamente.");
          setColorRtdo("#28a745"); // Verde para éxito
        })
        .catch((error) => {
          console.error("Error en la petición:", error);
        });
    } else if (state === "pending") {
      setMostrarRtdoModal(true);
      setTimeout(() => {
        setMostrarRtdoModal(false);
      }, 1500);

      setRtdo("Espere que se efectivice el pago");
      setColorRtdo("#b5a604"); // Amarillo para pendiente
    } else if (state === "failure") {
      setMostrarRtdoModal(true);
      setTimeout(() => {
        setMostrarRtdoModal(false);
      }, 1500);

      setRtdo("Algo salio mal!");
      setColorRtdo("#e60243"); // Rojo para error
    }
  }, [location]);

  useEffect(() => {
    fetch(`http://localhost:5000/api/Publicacion/byId?id=${id}`)
      .then((res) => res.json())
      .then((data) => {
        if (data.status === "Eliminada" && !user?.roles?.includes("Dueño")) {
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
  }, [id, user?.roles]);

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
      //Duplica la publicacion
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

      //Duplica los archivos
      // 1. Obtener imágenes desde la publicación original
      const imagenesRes = await fetch(
        `http://localhost:5000/api/Archivo/byEntidad?entidadId=${idPublicacion}&tipoEntidad=0`
      );
      const imagenesData = await imagenesRes.json();

      // 2. Subir imágenes nuevas a la nueva publicación duplicada
      for (const [i, img] of imagenesData.entries()) {
        // Convertir base64 a Blob
        const byteCharacters = atob(img.archivoBase64);
        const byteNumbers = new Array(byteCharacters.length)
          .fill(0)
          .map((_, idx) => byteCharacters.charCodeAt(idx));
        const byteArray = new Uint8Array(byteNumbers);
        const blob = new Blob([byteArray], { type: "image/jpeg" });

        // Crear FormData
        const formData = new FormData();
        formData.append("EntidadID", duplicarJson.idPublicacion);
        formData.append("TipoEntidad", 0);
        formData.append("Nombre", img.nombre || `Imagen ${i + 1}`);
        formData.append(
          "Descripcion",
          img.descripcion || `Imagen ${i + 1} de la publicación`
        );
        formData.append("Archivo", blob, `imagen${i + 1}.jpg`);

        // Enviar al backend
        await fetch("http://localhost:5000/api/Archivo", {
          method: "POST",
          body: formData,
        });
      }

      navigate(`/ModificarPublicacion/${duplicarJson.idPublicacion}`);
      if (!createRes.ok) throw new Error("Error al duplicar la publicacion");
    } catch (error) {
      console.error(error);
      alert("No se pudo duplicar la publicacion");
    }
  };

  if (!publicacion && !loadAuth) return <p>Cargando...</p>;

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
              <div className="title-container">
                <h2>{publicacion.titulo}</h2>

                {user?.roles?.includes("Dueño") && (
                  <p>
                    <strong>Estado: </strong> {publicacion.status}
                  </p>
                )}
              </div>
              <div className="button-container">
                {user?.roles?.includes("Dueño") && (
                  <>
                    <StyledButton
                      text="Modificar"
                      onClick={() =>
                        navigate(
                          `/ModificarPublicacion/${publicacion.idPublicacion}`
                        )
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
                  </>
                )}
                {!state &&
                  (user?.roles?.includes("Cliente") ||
                    user?.roles?.includes("Empleado")) && (
                    <StyledButton
                      text="Reservar"
                      onClick={() => setMostrarReservaModal(true)}
                    />
                  )}
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
                          {currentImageIndex + 1} / {imagenes.length}
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
                      : publicacion.maquina.tagsMaquina
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
            mensajeExito={
              exito
                ? "Se eliminó la publicación exitosamente"
                : mostrarRtdoModal
                ? "se aprobo el pago"
                : ""
            }
          />

          <ModalReserva
            publicacion={publicacion}
            precioPorDia={publicacion.precioPorDia}
            isOpen={mostrarReservaModal}
            onClose={() => setMostrarReservaModal(false)}
            onReservar={handleReservar}
          />

          <ModalResultado
            isOpen={mostrarRtdoModal}
            estado={rtdo}
            colorFondo={colorRtdo}
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
