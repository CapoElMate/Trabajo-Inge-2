import React from "react";
import "./DetalleAlquiler.css"; // Reutiliza los estilos de DetalleAlquiler
import StyledButton from "../CustomButton";

export default function DetalleReembolso({ reembolso, onGenerar }) {
  if (!reembolso) {
    return <p className="detalle-loading">Cargando reembolso...</p>;
  }

  return (
    <div className="detalle-alquiler-container">
      <h1>Detalle del Reembolso</h1>

      <div className="alquiler-info">
        <div>
          <h4>Motivo</h4>
          <p>{reembolso.motivo}</p>
        </div>

        <div>
          <h4>Fecha de cancelación</h4>
          <p>{reembolso.fechaCancelacion}</p>
        </div>

        <div>
          <h4>Monto a devolver</h4>
          <p>${reembolso.monto}</p>
        </div>
      </div>

      <StyledButton text="Generar Reembolso" onClick={onGenerar} />
    </div>
  );
}