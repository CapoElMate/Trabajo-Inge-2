import React from "react";
import "./Modal.css"; // Asegurate de que este archivo tenga el CSS anterior

export default function ConfirmModal({ isOpen, onClose, onConfirm, mensaje }) {
  if (!isOpen) return null;

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
      <div className="modal-content">
        <p>{mensaje}</p>
        <div style={{ display: "flex", justifyContent: "center", marginTop: 20 }}>
          <button
            style={buttonStyle}
            onMouseOver={handleMouseOver}
            onMouseOut={handleMouseOut}
            onClick={onConfirm}
          >
            Aceptar
          </button>
          <button
            style={buttonStyle}
            onMouseOver={handleMouseOver}
            onMouseOut={handleMouseOut}
            onClick={onClose}
          >
            Cancelar
          </button>
        </div>
      </div>
    </div>
  );
}