// src/components/Example.js (o donde hayas guardado tu Offcanvas)

import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { TbMenu2 } from "react-icons/tb"; 
import './SideBarMenu.css'; 
import { useAuth } from '../AuthContext';
function SideBarMenu() { // Renombrado para mayor claridad
  const [show, setShow] = useState(false);
  const {user} = useAuth();
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  return (
    <>
      <Button
        variant="link" // para que no tenga estilos predeterminados de botón de Bootstrap
        onClick={handleShow}
        className="menu-toggle-button" 
      >
        <TbMenu2 className="menu-icon" />
      </Button>

      <Offcanvas show={show} onHide={handleClose} className="custom-offcanvas">
        <Offcanvas.Header closeButton>
          <Offcanvas.Title>
            Menú 
          </Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <ul className="offcanvas-nav-list">
            <li><a href="/HomePage">Inicio</a></li>
            <li><a href="/Profile">Mi perfil</a></li>
            <li><a href="/Rentals">Mis reservas</a></li>
            
            <li><a href="/Leases">Mis alquileres</a></li>
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