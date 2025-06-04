import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import StyledButton from "../CustomButton";
import "./DetalleAlquiler.css";
import ConfirmModal from "../Modal";
import Header from "../Header";
import ModalReembolso from "../Reembolso/ModalReembolso";

export default function DetalleAlquiler() {
  const { id } = useParams();
  const navigate = useNavigate();
  const [alquiler, setAlquiler] = useState(null);
  const [loading, setLoading] = useState(true);
  const [modalReembolsoAbierto, setModalReembolsoAbierto] = useState(false);
  const [empleado, setEmpleado] = useState();
  const [cliente, setCliente] = useState();

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
    fetch("http://localhost:5000/api/Reembolso", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(reembolso),
    })
      .then((res) => {
        if (!res.ok) throw new Error("Error al generar el reembolso");
        return res.json();
      })
      .then((data) => {
        // Después de generar el reembolso exitosamente
        const alquilerCancelado = {
          ...alquiler,
          status: "Cancelado",
        };

        return fetch(
          `http://localhost:5000/api/Alquiler/byId?id=${alquiler.idAlquiler}`,
          {
            method: "PUT",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(alquilerCancelado),
          }
        );
      })
      .then((res) => {
        if (!res.ok)
          throw new Error("Error al actualizar el estado del alquiler");
        navigate("/HomePage");
      });
  };

  if (loading) return <p className="detalle-loading">Cargando...</p>;
  if (!alquiler)
    return <p className="detalle-error">No se encontró el alquiler.</p>;

  return (
    <>
      <Header />
      <div className="detalle-alquiler-container">
        <h1>Detalle del Alquiler</h1>
        {alquiler.status !== "Cancelado" && (
          <StyledButton
            text="Cancelar"
            onClick={() => setModalReembolsoAbierto(true)}
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
                {alquiler.publicacion.maquina.tagsMaquina
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
        />
      </div>
    </>
  );
}
