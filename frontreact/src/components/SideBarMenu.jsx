import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { TbMenu2 } from "react-icons/tb"; 
import './SideBarMenu.css'; 
import { useAuth } from '../AuthContext';
import { useNavigate } from 'react-router-dom';
function SideBarMenu() {
  const [show, setShow] = useState(false);
  const { user } = useAuth();
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const navigate = useNavigate();

  const handleNavigate = (path, restricted = false) => {
    if (restricted && !user) {
      navigate("/Login");
    } else {
      navigate(path);
    }
    setShow(false); // cerrar menú luego de navegación
  };

  // Elementos del menú por roles
  const menuItems = [];

  if (!user || user.roles.includes("Cliente")) {
    menuItems.push(
      { label: "Inicio", path: "/HomePage", restricted: false },
      { label: "Mi perfil", path: "/Perfil", restricted: true },
      { label: "Subir permiso de maquinaria", path: "/PermitFileUploeader", restricted: true },
      { label: "Cambiar contraseña", path: "/ForgotPassword", restricted: true },
      // { label: "Mis alquileres", path: "/Leases", restricted: true }
    );
  } else if (user.roles.includes("Empleado")) {
    menuItems.push(
      { label: "Inicio", path: "/EmployeeHome", restricted: false },
      { label: "Mi perfil", path: "/Perfil", restricted: false },
      { label: "Cambiar contraseña", path: "/ForgotPassword", restricted: true },
      { label: "Subir permiso de maquinaria", path: "/PermitFileUploeader", restricted: true },
      // { label: "Mis reservas", path: "/rentals", restricted: false },
      // { label: "Mis alquileres", path: "/Leases", restricted: false },
      { label: "Publicaciones", path: "/HomePage", restricted: false },
      { label: "Filtrar Reservas", path: "/FiltrarReservas", restricted: false },
      { label: "Filtrar Alquileres", path: "/FiltrarAlquileres", restricted: false },
      { title: "Validar Usuarios", path: "/solicitudesDeRegistro" },
      { title: "Solicitudes de permisos", path: "/PermisosDeUsuarios" },
    );
  } else if (user.roles.includes("Dueño")) {
    menuItems.push(
      { label: "Inicio", path: "/HomePageAdmin", restricted: false },
      { label: "Cambiar contraseña", path: "/ForgotPassword", restricted: true },
      { label: "Publicaciones", path: "/HomePage", restricted: false },
      { label: "Maquinarias", path: "/ListarMaquinaria", restricted: false },
      { label: "Crear Maquinaria", path: "/CrearMaquinaria", restricted: false },
      { label: "Crear Publicacion", path: "/CrearPublicacion", restricted: false },
      { label: "Filtrar Reservas", path: "/FiltrarReservas", restricted: false },
      { label: "Filtrar Alquileres", path: "/FiltrarAlquileres", restricted: false },
      { label: "Registrar empleado", path: "/SignUpEmpleado", restricted: false },
      // { label: "Mi perfil", path: "/Profile", restricted: false },
      //{ label: "Publicaciones", path: "/HomePage", restricted: false }
    );
  }

  return (
    <>
      <Button
        variant="link"
        onClick={handleShow}
        className="menu-toggle-button"
      >
        <TbMenu2 className="menu-icon" />
      </Button>

      <Offcanvas show={show} onHide={handleClose} className="custom-offcanvas">
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>Menú</Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <ul className="offcanvas-nav-list">
            {menuItems.map((item, index) => (
              <li key={index} onClick={() => handleNavigate(item.path, item.restricted)}>
                <a>{item.label}</a>
              </li>
            ))}
          </ul>
          <p>
            <small>Horario de atención: L-V 9-18hs</small>
          </p>
        </Offcanvas.Body>
      </Offcanvas>
    </>
  );
}

export default SideBarMenu;
