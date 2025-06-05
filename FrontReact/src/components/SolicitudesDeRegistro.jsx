// src/components/SolicitudesDeRegistro.jsx
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

const SolicitudesDeRegistro = () => {
  const [usuarios, setUsuarios] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUsuarios = async () => {
      try {
        const response = await fetch('http://localhost:5000/api/Usuario/all');
        const data = await response.json();
        const noVerificados = data.filter(u => u.dniVerificado === false);
        setUsuarios(noVerificados);
      } catch (error) {
        console.error('Error al obtener usuarios:', error);
      }
    };

    fetchUsuarios();
  }, []);

  const handleClick = (dni) => {
    navigate(`/DetalleUsuario/${dni}`);
  };

  return (
    <div className="p-4 space-y-4">
      <h2 className="text-2xl font-bold">Solicitudes de Registro</h2>
      {usuarios.map(usuario => (
        <div
          key={usuario.dni}
          className="border p-4 rounded shadow hover:bg-gray-100 cursor-pointer"
          onClick={() => handleClick(usuario.dni)}
        >
          <p><strong>Nombre:</strong> {usuario.nombre} {usuario.apellido}</p>
          <p><strong>Email:</strong> {usuario.email}</p>
          <p><strong>DNI:</strong> {usuario.dni}</p>
        </div>
      ))}
      {usuarios.length === 0 && <p>No hay solicitudes pendientes.</p>}
    </div>
  );
};

export default SolicitudesDeRegistro;
