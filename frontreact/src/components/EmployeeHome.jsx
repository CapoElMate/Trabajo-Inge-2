import Header from "./Header";
import { useNavigate } from "react-router-dom";
import { useState } from "react";

function EmployeeHome() {
  const navigate = useNavigate();
  const [hoveredCard, setHoveredCard] = useState(null);

  const containerStyle = {
    display: "flex",
    justifyContent: "center",
    flexWrap: "wrap",
    marginTop: "2rem",
    gap: "2rem",
  };

  const baseCardStyle = {
    cursor: "pointer",
    borderRadius: "12px",
    padding: "2rem",
    width: "250px",
    height: "150px",
    boxShadow: "0 4px 8px rgba(0,0,0,0.1)",
    textAlign: "center",
    fontSize: "1.2rem",
    fontWeight: "bold",
    transition: "all 0.3s ease",
    display: "flex",
    flexDirection: "column",
    justifyContent: "center",
  };

  const handleNavigate = (path) => {
    navigate(path);
  };

  const cards = [
    { title: "Filtrar Reservas", path: "/FiltrarReservas" },
    { title: "Filtrar Alquileres", path: "/FiltrarAlquileres" },
    { title: "Validar Usuarios", path: "/solicitudesDeRegistro" },
    { title: "Solicitudes de permisos", path: "/PermisosDeUsuarios" },
  ];

  return (
    <>
      <Header />
      <div style={containerStyle}>
        {cards.map((card, index) => {
          const isHovered = hoveredCard === index;
          const dynamicStyle = {
            ...baseCardStyle,
            backgroundColor: isHovered ? "#cc0000" : "#f0f0f0",
            color: isHovered ? "#ffffff" : "#000000",
          };

          return (
            <div
              key={index}
              style={dynamicStyle}
              onClick={() => handleNavigate(card.path)}
              onMouseEnter={() => setHoveredCard(index)}
              onMouseLeave={() => setHoveredCard(null)}
            >
              
              {isHovered && <div style={{ marginTop: "0.5rem", fontSize: "1.3rem" }}>Ir a </div>}
                <div>{card.title}</div>
            </div>
          );
        })}
      </div>
    </>
  );
}

export default EmployeeHome;
