import React from "react";

export default function StyledButton({ text, onClick }) {
  return (
    <button
      style={{
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
      }}
      onMouseOver={(e) => (e.target.style.backgroundColor = "#b32a36")}
      onMouseOut={(e) => (e.target.style.backgroundColor = "#dc3545")}
      onClick={onClick}
    >
      {text}
    </button>
  );
}