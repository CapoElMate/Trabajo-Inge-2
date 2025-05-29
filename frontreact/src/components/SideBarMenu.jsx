
import React, { useState } from 'react';
import Button from 'react-bootstrap/Button';
import Offcanvas from 'react-bootstrap/Offcanvas';
import { TbMenu2 } from "react-icons/tb"; 
import './SideBarMenu.css'; 
import { useAuth } from '../AuthContext';
import { useNavigate } from 'react-router-dom';
function SideBarMenu() { // Renombrado para mayor claridad
  const [show, setShow] = useState(false);
  const {user} = useAuth();
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const navigate = useNavigate();
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
          <Offcanvas.Title>
            Menú 
          </Offcanvas.Title>
        </Offcanvas.Header>
        <Offcanvas.Body>
          <ul className="offcanvas-nav-list">
            <li onClick={()=>{navigate("/HomePage")}}><a>Inicio</a></li>
            <li onClick={()=>{navigate("/Profile")}} ><a>Mi perfil</a></li>
            <li onClick={()=>{navigate("/rentals")}}><a>Mis reservas</a></li>
            <li onClick={()=>{navigate("/Leases")}}><a>Mis alquileres</a></li>
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