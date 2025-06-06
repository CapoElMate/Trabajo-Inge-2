// pages/DetalleReserva.jsx
import { useParams, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import Header from "../Header";
import StyledButton from "../CustomButton";
import ConfirmModal from "../Modal";
import "./DetalleReserva.css";
import { useAuth } from "../../AuthContext";

export default function DetalleReserva() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [reserva, setReserva] = useState(null);
  const [publicacion, setPublicacion] = useState();
  const [confirmModal, setConfirmModal] = useState(false);
  const { user, loadAuth } = useAuth();
  const [error, setError] = useState(null);
  const [exito, setExito] = useState(null);
  const [empleado, setEmpleado] = useState();

  useEffect(() => {
    if (!user?.userName) return; // Espera a que user esté definido

    const fetchCliente = async () => {
      try {
        const getDNI = await fetch(
          `http://localhost:5000/api/Usuario/byEmail?email=${user.userName}`
        )
          .then((res) => res.json())
          .then((data) => data.dni);

        const response = await fetch(
          `http://localhost:5000/api/Cliente/byDNI?DNI=${getDNI}`
        );
        if (!response.ok) throw new Error("Error al obtener el cliente");
        const data = await response.json();
        setEmpleado(data);
      } catch (error) {
        console.error("Error al cargar el cliente:", error);
      }
    };

    fetchCliente();
  }, [user?.userName]);

  const handleEfectivizar = async () => {
    // 1 - Tomar reserva y pasarla a efectivizada
    const reservaResponse = await fetch(
      `http://localhost:5000/api/Reserva/byId?id=${id}`
    );

    if (!reservaResponse.ok) throw new Error("Error al obtener la reserva");

    const reservaData = await reservaResponse.json();

    // 2 - Validar que la reserva no es del empleado.
    const empleadoResponse = await fetch(
      `http://localhost:5000/api/Empleado/byEmail?email=${user.userName}`
    );
    if (!empleadoResponse.ok) throw new Error("Error al obtener el empleado");
    const empleadoData = await empleadoResponse.json();
    if (empleadoData.cliente.usuarioRegistrado.dni === reservaData.dniCliente) {
      setError("No puedes efectivizar tus propias reservas.");
      setTimeout(() => {
        setConfirmModal(false);
        setError(false);
      }, 2000);

      return;
    }

    // 3 - Crear alquiler
    const now = new Date().toISOString();

    const newAlquiler = {
      fecEfectivizacion: now,
      status: "Efectivizado",
      dniCliente: reservaData.dniCliente,
      dniEmpleadoEfectivizo: empleado.usuarioRegistrado.dni,
      infoAsentada: [],
      reserva: reservaData,
    };

    const alquilerResponse = await fetch(`http://localhost:5000/api/Alquiler`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(newAlquiler),
    });

    if (!alquilerResponse.ok) throw new Error("Error al crear el alquiler");

    reservaData.status = "Efectivizada";
    const resAlquiler = await alquilerResponse.json();
    reservaData.idAlquiler = resAlquiler.idAlquiler;

    //Actualiza reserva
    const updateReservaResponse = await fetch(
      `http://localhost:5000/api/Reserva?id=${id}`,
      {
        method: "PUT",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(reservaData),
      }
    ).then((res) => {

      setExito("El alquiler se efectivizo satisfactoriamente.");
      setTimeout(() => {
        setConfirmModal(false);
        setExito(false);
        navigate("/FiltrarReservas");
      }, 2000);

    });
    if (!updateReservaResponse.ok)
      throw new Error("Error al actualizar la reserva");
  };

  useEffect(() => {
    const fetchReservaYPublicacion = async () => {
      try {
        const resReserva = await fetch(
          `http://localhost:5000/api/Reserva/byId?id=${id}`
        );
        if (!resReserva.ok) throw new Error("Error al obtener la reserva");

        const dataReserva = await resReserva.json();
        setReserva(dataReserva);

        if (dataReserva.idPublicacion) {
          const resPublicacion = await fetch(
            `http://localhost:5000/api/Publicacion/byId?id=${dataReserva.idPublicacion}`
          );
          if (!resPublicacion.ok)
            throw new Error("Error al obtener la publicación");

          const dataPublicacion = await resPublicacion.json();
          setPublicacion(dataPublicacion);
        }
      } catch (error) {
        console.error("Fallo al cargar los datos:", error);
      }
    };

    fetchReservaYPublicacion();
  }, [id]);

  if (!reserva || !publicacion || !publicacion.maquina)
    return <p>Cargando...</p>;

  return (
    <>
      <Header />
      <div className="p-4 detalle-reserva-contenedor">
        <div className="header">
          <h2>Detalle de la reserva</h2>
          <div className="button-container">
            {user?.roles?.includes("Empleado") && (
              <StyledButton
                text="Efectivizar"
                onClick={() => setConfirmModal(true)}
              />
            )}
          </div>
        </div>

        <div
          className={`reserva-info ${
            reserva.calle && reserva.calle.trim() !== "" ? "con-direccion" : ""
          }`}
        >
          <div className="fechas">
            <h4>Fechas</h4>
            <p>Desde: {reserva.fecInicio.slice(0, 10)}</p>
            <p>Hasta: {reserva.fecFin.slice(0, 10)}</p>
          </div>

          <div>
            <h4>Estado</h4>
            <p>{reserva.status}</p>
          </div>

          {reserva.calle && reserva.calle.trim() !== "" && (
            <div>
              <h4>Dirección</h4>
              <p>
                {reserva.calle} {reserva.altura}
                {reserva.piso && `, Piso ${reserva.piso}`}
                {reserva.dpto && `, Dpto ${reserva.dpto}`}
              </p>
            </div>
          )}

          <div>
            <h4>Monto pagado</h4>
            <p>${reserva.montoTotal}</p>
          </div>
        </div>
        <div className="maquinaria-container">
          <h3 className="maquinaria-title">Maquinaria</h3>

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
            {/* <div className="maquinaria-item"></div> */}
          </div>
        </div>
      </div>
      <ConfirmModal
        isOpen={confirmModal}
        onClose={() => setConfirmModal(false)}
        onConfirm={() => {
          handleEfectivizar();
        }}
        mensaje="¿Estás seguro de desea efectivizar este alquiler?"
        mensajeError={error}
        mensajeExito={exito}
      />
    </>
  );
}
