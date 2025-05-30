import { useNavigate } from "react-router-dom";
import React, { useState } from 'react'; // Import useState for hover effect
import Header from "./Header";
import './HomePageAdmin.css'; // We'll create this CSS file

function HomePageAdmin() {
  const navigate = useNavigate();
  const [isHovered, setIsHovered] = useState(false); // State to track hover

  return (
    <>
      <Header />
      <div className="admin-homepage-container">
        <div
          className="admin-action-card"
          onClick={() => navigate("/SignUp")}
          onMouseEnter={() => setIsHovered(true)} // Set hovered to true on mouse enter
          onMouseLeave={() => setIsHovered(false)} // Set hovered to false on mouse leave
        >
          {isHovered ? (
            <span className="card-text">Ir a Registrar un Empleado</span>
          ) : (
            <span className="card-text">Registrar un Empleado</span>
          )}
        </div>

        {/* You can add more action cards here if needed */}
        {/*
        <div
          className="admin-action-card"
          onClick={() => navigate("/SomeOtherAdminPage")}
          onMouseEnter={() => setIsHoveredAnother(true)}
          onMouseLeave={() => setIsHoveredAnother(false)}
        >
          {isHoveredAnother ? (
            <span className="card-text">Ir a Otra Acción de Admin</span>
          ) : (
            <span className="card-text">Gestionar Publicaciones</span>
          )}
        </div>
        */}
      </div>
    </>
  );
}

export default HomePageAdmin;