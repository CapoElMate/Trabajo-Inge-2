import { useNavigate } from "react-router-dom";
import React, { useState } from "react"; // Import useState for hover effect
import Header from "./Header";
import "./HomePageAdmin.css"; // We'll create this CSS file

function HomePageAdmin() {
  const navigate = useNavigate();
  const [isHovered, setIsHovered] = useState(false); // State to track hover

  return (
    <>
      <Header />
      <div className="admin-homepage-container">
        <div
          className="admin-action-card"
          onClick={() => navigate("/SignUpEmpleado")}
          onMouseEnter={() => setIsHovered(true)} // Set hovered to true on mouse enter
          onMouseLeave={() => setIsHovered(false)} // Set hovered to false on mouse leave
        >
          {/* {isHovered ? (
            <span className="card-text">Ir a Registrar un Empleado</span>
          ) : ( */}
          <span className="card-text">Registrar un Empleado</span>
          {/* )} */}
        </div>
        <div
          className="admin-action-card"
          onClick={() => navigate("/CrearMaquinaria")}
          // onMouseEnter={() => setIsHoveredAnother(true)}
          // onMouseLeave={() => setIsHoveredAnother(false)}
        >
          {/* {isHovered ? ( */}
          {/* <span className="card-text">Ir a Crear una Maquinaria</span>
          ) : ( */}
          <span className="card-text">Crear Maquinaria</span>
          {/* )} */}
        </div>

        <div
          className="admin-action-card"
          onClick={() => navigate("/ListarMaquinaria")}
          // onMouseEnter={() => setIsHoveredAnother(true)}
          // onMouseLeave={() => setIsHoveredAnother(false)}
        >
          {/* {isHovered ? ( */}
          {/* <span className="card-text">Ir a Crear una Maquinaria</span>
          ) : ( */}
          <span className="card-text">Maquinaria</span>
          {/* )} */}
        </div>
        <div
          className="admin-action-card"
          onClick={() => navigate("/CrearPublicacion")}
          // onMouseEnter={() => setIsHoveredAnother(true)}
          // onMouseLeave={() => setIsHoveredAnother(false)}
        >
          {/* {isHovered ? (
            <span className="card-text">Ir a Crear Publicacion</span> */}
          {/* ) : ( */}
          <span className="card-text">Crear Publicacion</span>
          {/* )} */}
        </div>

        <div
          className="admin-action-card"
          onClick={() => navigate("/HomePage")}
          // onMouseEnter={() => setIsHoveredAnother(true)}
          // onMouseLeave={() => setIsHoveredAnother(false)}
        >
          {/* {isHovered ? (
            <span className="card-text">Ir a Publicaciones</span>
          ) : ( */}
          <span className="card-text">Publicaciones</span>
          {/* )} */}
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
