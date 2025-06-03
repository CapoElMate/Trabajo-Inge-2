const NoAutorizado = () => {
  return (
    <div style={{ padding: "2rem", textAlign: "center" }}>
      <h1>403 - No autorizado</h1>
      <p>No tenés permiso para acceder a esta página.</p>
    </div>
  );
};

export default NoAutorizado;

// NoAutorizado.jsx
import React from 'react';

const containerStyle = {
  padding: "3rem",
  textAlign: "center",
  backgroundColor: "#ffffff",
  borderRadius: "8px",
  boxShadow: "0 4px 12px rgba(0, 0, 0, 0.1)",
  maxWidth: "500px",
  margin: "20px auto", // "auto" for horizontal centering if not using flexbox on body
  display: "flex", // Adding flexbox for internal centering if you don't apply it to body
  flexDirection: "column",
  justifyContent: "center",
  alignItems: "center",
  minHeight: "100vh" // If you apply styles directly to the div
};

const h1Style = {
  fontSize: "3.5rem",
  color: "#e74c3c",
  marginBottom: "1rem"
};

const pStyle = {
  fontSize: "1.2rem",
  color: "#555",
  lineHeight: "1.5"
};

const linkStyle = {
  display: "inline-block",
  marginTop: "1.5rem",
  padding: "0.8rem 1.5rem",
  backgroundColor: "#3498db",
  color: "#ffffff",
  textDecoration: "none",
  borderRadius: "5px",
  transition: "background-color 0.3s ease"
};

const NoAutorizado = () => {
  return (
    <div style={containerStyle}>
      <h1 style={h1Style}>403 - No autorizado</h1>
      <p style={pStyle}>No tenés permiso para acceder a esta página.</p>
    </div>
  );
};

export default NoAutorizado;