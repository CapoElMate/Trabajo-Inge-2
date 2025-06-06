import React, { useState, useEffect } from "react";
import "../Modal.css";
import "../Reserva/ModalReserva.css";

export default function ModalReembolso({
  isOpen,
  onClose,
  onConfirmar,
  dataAlquiler,
  dataCliente,
  mensajeError = "",
  mensajeExito = "",
}) {
  const [motivo, setMotivo] = useState("");
  const [fecCancelacion, setFecCancelacion] = useState("");
  const [alquiler, setAlquiler] = useState(null);
  const [cliente, setCliente] = useState(null);
  const [reembolso, setReembolso] = useState(null);
  const [confirmado, setConfirmado] = useState(false);
  const [fechaError, setFechaError] = useState("");
  const [formError, setFormError] = useState("");

  useEffect(() => {
    if (isOpen) {
      setAlquiler(dataAlquiler);
      setCliente(dataCliente);
      setMotivo("");
      setFecCancelacion("");
      setReembolso(null);
      setConfirmado(false);
      setFechaError("");
      setFormError("");
    }
  }, [isOpen, dataAlquiler, dataCliente]);

  if (!isOpen) return null;

  const handleConfirmar = () => {
    if (!motivo || !fecCancelacion) {
      setFormError("Por favor, completa todos los campos.");
      return;
    }

    const inicio = new Date(alquiler.reserva.fecInicio);
    const fin = new Date(alquiler.reserva.fecFin);
    const cancelacion = new Date(fecCancelacion);

    if (cancelacion > fin) {
      setFechaError(
        "La fecha de cancelación no puede ser posterior a la fecha de fin del alquiler."
      );
      return;
    }

    if (cancelacion < inicio) {
      setFechaError(
        "La fecha de cancelación no puede ser anterior a la fecha de inicio del alquiler."
      );
      return;
    }

    // Todo correcto, limpiar errores
    setFechaError("");
    setFormError("");

    const totalDias = Math.ceil((fin - inicio) / (1000 * 60 * 60 * 24));
    const diasRestantes = Math.ceil(
      (fin - cancelacion) / (1000 * 60 * 60 * 24)
    );
    const montoPorDia = alquiler.reserva.montoTotal / totalDias;
    const montoADevolver = montoPorDia * diasRestantes;

    const generado = {
      motivo,
      fecCancelacion,
      monto: Math.round(montoADevolver),
      idAlquiler: alquiler.idAlquiler,
      dniCliente: cliente.dni,
      status: "Pendiente de pago",
    };

    setReembolso(generado);
    setConfirmado(true);
  };

  const handleMotivoChange = (e) => {
    setMotivo(e.target.value);
    if (e.target.value && fecCancelacion) {
      setFormError("");
    }
  };

  const handleFechaChange = (e) => {
    setFecCancelacion(e.target.value);
    setFechaError("");
    if (motivo && e.target.value) {
      setFormError("");
    }
  };

  const buttonStyle = {
    backgroundColor: "#dc3545",
    color: "white",
    padding: "10px 16px",
    border: "none",
    borderRadius: "8px",
    cursor: "pointer",
    fontSize: "16px",
    fontWeight: "500",
    boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
    transition: "background-color 0.2s ease",
    margin: "0 8px",
  };

  const handleMouseOver = (e) => {
    e.target.style.backgroundColor = "#b32a36";
  };

  const handleMouseOut = (e) => {
    e.target.style.backgroundColor = "#dc3545";
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content" style={{ maxWidth: 400 }}>
        {mensajeError ? (
          <div
            className="error-message"
            style={{ color: "red", marginBottom: 16 }}
          >
            {mensajeError}
          </div>
        ) : mensajeExito ? (
          <div
            className="success-message"
            style={{ color: "green", marginBottom: 16 }}
          >
            {mensajeExito}
          </div>
        ) : (
          <>
            <h2>Gestión de Reembolso</h2>

            <label htmlFor="motivo">Motivo:</label>
            <textarea
              id="motivo"
              value={motivo}
              onChange={handleMotivoChange}
              style={{
                width: "100%",
                padding: 8,
                marginBottom: 12,
                boxSizing: "border-box",
                minHeight: 80,
                resize: "vertical",
              }}
              disabled={confirmado}
            />

            <label htmlFor="fechaCancelacion">Fecha de cancelación:</label>
            <input
              type="date"
              id="fechaCancelacion"
              value={fecCancelacion}
              onChange={handleFechaChange}
              style={{
                width: "100%",
                padding: 8,
                marginBottom: 8,
                boxSizing: "border-box",
              }}
              disabled={confirmado}
            />

            {fechaError && (
              <div style={{ color: "red", marginBottom: 12 }}>{fechaError}</div>
            )}

            {formError && (
              <div style={{ color: "red", marginBottom: 12 }}>{formError}</div>
            )}

            {confirmado && reembolso && (
              <div style={{ marginBottom: 16 }}>
                <strong>Monto a devolver:</strong> ${reembolso.monto}
              </div>
            )}

            <div style={{ display: "flex", justifyContent: "center" }}>
              {!confirmado ? (
                <>
                  <button
                    style={buttonStyle}
                    onMouseOver={handleMouseOver}
                    onMouseOut={handleMouseOut}
                    onClick={onClose}
                  >
                    Atrás
                  </button>
                  <button
                    style={buttonStyle}
                    onMouseOver={handleMouseOver}
                    onMouseOut={handleMouseOut}
                    onClick={handleConfirmar}
                  >
                    Confirmar
                  </button>
                </>
              ) : (
                <button
                  style={buttonStyle}
                  onMouseOver={handleMouseOver}
                  onMouseOut={handleMouseOut}
                  onClick={() => onConfirmar(reembolso)}
                >
                  Generar Reembolso
                </button>
              )}
            </div>
          </>
        )}
      </div>
    </div>
  );
}
