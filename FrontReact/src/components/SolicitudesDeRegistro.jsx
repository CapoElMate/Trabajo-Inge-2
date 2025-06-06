// src/components/SolicitudesDeRegistro.jsx
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
const SolicitudesDeRegistro = () => {
  const [usuarios, setUsuarios] = useState([]);
  const [usuariosFiltrados, setUsuariosFiltrados] = useState([]);
  const [dniFiltro, setDniFiltro] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUsuarios = async () => {
      try {
        const response = await fetch('http://localhost:5000/api/Usuario/all');
        const data = await response.json();
        const noVerificados = data.filter(u => u.dniVerificado === false);
        setUsuarios(noVerificados);
        setUsuariosFiltrados(noVerificados);
      } catch (error) {
        console.error('Error al obtener usuarios:', error);
      }
    };

    fetchUsuarios();
  }, []);

  const handleClick = (dni) => {
    navigate(`/DetalleUsuario/${dni}`);
  };

  const handleFiltrar = () => {
    if (dniFiltro.trim() === '') {
      setUsuariosFiltrados(usuarios);
    } else {
      const filtrados = usuarios.filter(u => u.dni.toString().includes(dniFiltro.trim()));
      setUsuariosFiltrados(filtrados);
    }
  };

  return (
    <> <Header/>
    <div className="p-4 space-y-4">
      <h1 className="text-2xl font-bold">Solicitudes de Registro</h1>

      {/* Filtro por DNI */}
      <div className="flex items-center gap-2 mb-4">
        <h2>Filtrar por DNI</h2>
        <input
          type="text"
          placeholder="Filtrar por DNI"
          value={dniFiltro}
          onChange={(e) => setDniFiltro(e.target.value)}
          className="border px-3 py-1 rounded"
        />
        <button style={{backgroundColor:'#cc0000',color:'white'}}
          onClick={handleFiltrar}
        >
          Filtrar
        </button>
      </div>

      {/* Lista de usuarios */}
      {usuariosFiltrados.map(usuario => (
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

      {usuariosFiltrados.length === 0 && <p>No hay solicitudes pendientes.</p>}
    </div>
  </>);
};

export default SolicitudesDeRegistro;
