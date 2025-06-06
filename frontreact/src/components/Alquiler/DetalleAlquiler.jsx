import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import StyledButton from "../CustomButton";
import "./DetalleAlquiler.css";
import ConfirmModal from "../Modal";
import Header from "../Header";
import ModalReembolso from "../Reembolso/ModalReembolso";
import { useAuth } from "../../AuthContext";

export default function DetalleAlquiler() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [alquiler, setAlquiler] = useState(null);
  const [loading, setLoading] = useState(true);
  const [modalReembolsoAbierto, setModalReembolsoAbierto] = useState(false);
  const [empleado, setEmpleado] = useState();
  const [cliente, setCliente] = useState();
  const [userEmpleado, setUserEmpleado] = useState();
  const [publicacion, setPublicacion] = useState();
  const { user } = useAuth();
  const [error, setError] = useState(null);
  const [exito, setExito] = useState(null);
  
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
        setUserEmpleado(data);
      } catch (error) {
        console.error("Error al cargar el cliente:", error);
      }
    };

    fetchCliente();
  }, [user?.userName]);
  
  useEffect(() => {
    const fetchData = async () => {
      try {
        // Primero, traemos el alquiler
        const resAlquiler = await fetch(
          `http://localhost:5000/api/Alquiler/byId?id=${id}`
        );
        const alquilerData = await resAlquiler.json();

        // Luego, traemos la publicación si el alquiler fue encontrado

        const resPubli = await fetch(
          `http://localhost:5000/api/Publicacion/byId?id=${alquilerData.reserva.idPublicacion}`
        );
        const publicacion = await resPubli.json();

        setAlquiler({ ...alquilerData, publicacion });

        const resEmp = await fetch(
          `http://localhost:5000/api/Empleado/byDNI?DNI=${alquilerData.dniEmpleadoEfectivizo}`
        );
        const empleado = await resEmp.json();

        setEmpleado(empleado.cliente.usuarioRegistrado);

        const resCli = await fetch(
          `http://localhost:5000/api/Cliente/byDNI?DNI=${alquilerData.dniCliente}`
        );
        const cliente = await resCli.json();

        setCliente(cliente.usuarioRegistrado);
      } catch (error) {
        console.error(
          "Error al cargar datos del alquiler o publicación:",
          error
        );
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  const handleReembolso = (reembolso) => {
    const cancelarYReembolsar = async () => {
      try {
        //1. Crear reembolso
        const resReembolso = await fetch(
          "http://localhost:5000/api/Reembolso",
          {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(reembolso),
          }
        );
        if (!resReembolso.ok) throw new Error("Error al generar el reembolso");

        // 2. Cancelar alquiler
        const alquilerCancelado = {
          ...alquiler,
          status: "Cancelado",
        };

        const resAlquiler = await fetch(
          `http://localhost:5000/api/Alquiler/byId?id=${alquiler.idAlquiler}`,
          {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(alquilerCancelado),
          }
        );
        if (!resAlquiler.ok)
          throw new Error("Error al actualizar el estado del alquiler");

        // 3. Actualizar publicación
        const publicacionActualizada = {
          ...alquiler.publicacion,
          status: "Disponible",
        };

        const resPublicacion = await fetch(
          `http://localhost:5000/api/Publicacion/byId?id=${publicacionActualizada.idPublicacion}`,
          {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(publicacionActualizada),
          }
        );
        if (!resPublicacion.ok)
          throw new Error("Error al actualizar la publicación");

        // 4. Actualizar maquinaria
        const maquinariaActualizada = {
          ...alquiler.publicacion.maquina,
          status: "Disponible",
        };

        const resMaquina = await fetch(
          `http://localhost:5000/api/Maquinas/byId?id=${maquinariaActualizada.idMaquina}`,
          {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(maquinariaActualizada),
          }
        );
        if (!resMaquina.ok)
          throw new Error("Error al actualizar la maquinaria");

        // 5. Actualizar el estado si lo necesitás
        setPublicacion(publicacionActualizada);
        setExito("Reembolso generado exitosamente.");
        setTimeout(() => {
          navigate("/HomePage");
        }, 2000);
        // // 6. Navegar
      } catch (error) {
        console.error("Error en proceso de cancelación:", error);
        // mostrar alerta o toast si querés
      }
    };

    cancelarYReembolsar();
  };

  const handleCancelar = () => {
    
    if(alquiler.reserva.dniCliente === userEmpleado.usuarioRegistrado.dni) {
      setError("No puede cancelar alquileres que le pertenezcan.");
      setModalReembolsoAbierto(true);
      setTimeout(() => {
        setModalReembolsoAbierto(false);
        setError(false);
      }, 2000);
      return;   
    }
    setModalReembolsoAbierto(true);
  }

  if (loading) return <p className="detalle-loading">Cargando...</p>;
  if (!alquiler)
    return <p className="detalle-error">No se encontró el alquiler.</p>;

  return (
    <>
      <Header />
      <div className="detalle-alquiler-container">
        <h1>Detalle del Alquiler</h1>
        {alquiler.status !== "Cancelado" &&
          user?.roles?.includes("Empleado") && (
            <StyledButton
              text="Cancelar"
              onClick={() => handleCancelar()}
            />
          )}
        <div
          className={`alquiler-info ${
            alquiler.calle && alquiler.calle.trim() !== ""
              ? "con-direccion"
              : ""
          }`}
        >
          <div className="fechas">
            <h4>Fechas</h4>
            <p>Desde: {alquiler.reserva.fecInicio.slice(0, 10)}</p>
            <p>Hasta: {alquiler.reserva.fecFin.slice(0, 10)}</p>
          </div>

          <div>
            <h4>Estado</h4>
            <p>{alquiler.status}</p>
          </div>

          {alquiler.calle && alquiler.calle.trim() !== "" && (
            <div>
              <h4>Dirección</h4>
              <p>
                {alquiler.calle} {alquiler.altura}
                {alquiler.piso && `, Piso ${alquiler.piso}`}
                {alquiler.dpto && `, Dpto ${alquiler.dpto}`}
              </p>
            </div>
          )}

          <div>
            <h4>Datos Adicionales</h4>
            <p>
              Cliente: {cliente.nombre} {cliente.apellido} - DNI: {cliente.dni}
            </p>
            <p>
              Empleado que efectivizo: {empleado.nombre} {empleado.apellido}
            </p>
          </div>
        </div>

        <div className="maquinaria-container">
          <h3 className="maquinaria-title">Maquinaria</h3>

          {/* Fila 1 */}
          <div className="maquinaria-row">
            <div className="maquinaria-item">
              <h4>ID</h4>
              <p>{alquiler.publicacion.maquina.idMaquina}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Estado</h4>
              <p>{alquiler.publicacion.maquina.status}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Año de Fabricación</h4>
              <p>{alquiler.publicacion.maquina.anioFabricacion}</p>
            </div>
          </div>

          {/* Fila 2 */}
          <div className="maquinaria-row">
            <div className="maquinaria-item">
              <h4>Modelo</h4>
              <p>{alquiler.publicacion.maquina.modelo.modelo}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Marca</h4>
              <p>{alquiler.publicacion.maquina.modelo.marca.marca}</p>
            </div>
            <div className="maquinaria-item">
              <h4>Tipo</h4>
              <p>{alquiler.publicacion.maquina.tipoMaquina.tipo}</p>
            </div>
          </div>

          {/* Fila 3 */}
          <div className="maquinaria-row">
            <div className="maquinaria-item">
              <h4>Tags</h4>
              <p>
                {alquiler.publicacion.maquina.tagsMaquina.length === 0
                ? "N/A"
                : alquiler.publicacion.maquina.tagsMaquina
                  .map((t) => t.tag)
                  .join(", ")}
              </p>
            </div>
            <div className="maquinaria-item">
              <h4>Permisos Especiales</h4>
              <p>
                {alquiler.publicacion.maquina.permisosEspeciales.length === 0
                  ? "N/A"
                  : alquiler.publicacion.maquina.permisosEspeciales
                      .map((p) => p.permiso)
                      .join(", ")}
              </p>
            </div>
          </div>
        </div>

        <ModalReembolso
          isOpen={modalReembolsoAbierto}
          onClose={() => setModalReembolsoAbierto(false)}
          onConfirmar={handleReembolso}
          dataAlquiler={alquiler}
          dataCliente={cliente}
          mensajeError= {error}
          mensajeExito={exito}
        />
      </div>
    </>
  );
}
