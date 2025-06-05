import React, { useEffect, useState } from 'react';
import { TbHandClick } from 'react-icons/tb';
import { useParams, useNavigate } from 'react-router-dom';

const DetalleUsuario = () => {
    const { dni } = useParams();
    const navigate = useNavigate();
    const [archivo, setArchivo] = useState(null);
    const [usuario, setUsuario] = useState(null);
    const [error, setError] = useState(null);

    const handClick = async () => {
        try {
            const respuesta = await fetch(`http://localhost:5000/api/Cliente/ConfirmDNI?dni=${dni}`, {
                method: 'PUT',
                headers: {
                    'Accept': 'text/plain',
                }
            });

            if (!respuesta.ok) {
                throw new Error(`HTTP ${respuesta.status} - ${respuesta.statusText}`);
            }

            const mensaje = await respuesta.text();
            console.log('Respuesta del servidor:', mensaje);
            alert("Se ha registrado el usuario con éxito");
            navigate("/SolicitudesDeRegistro");
        } catch (error) {
            console.error('Error al validar el DNI:', error);
            alert(`Error al validar el DNI: ${error.message}`);
        }
    };

    const handleRechazar = async () => {
        try {
            const clienteRes = await fetch(`http://localhost:5000/api/Cliente/byDNI?dni=${dni}`, {
                method: 'DELETE'
            });

            if (!clienteRes.ok) throw new Error('No se pudo eliminar el cliente');

            const usuarioRes = await fetch(`http://localhost:5000/api/Usuario/byDNI?dni=${dni}`, {
                method: 'DELETE'
            });

            if (!usuarioRes.ok) throw new Error('No se pudo eliminar el usuario');

            alert('Se ha rechazado la solicitud de registro con éxito');
            navigate('/SolicitudesDeRegistro');
        } catch (err) {
            console.error(err);
            alert(`Error al rechazar la solicitud: ${err.message}`);
        }
    };

    useEffect(() => {
        const fetchDatos = async () => {
            try {
                const archivoRes = await fetch(`http://localhost:5000/api/Archivo/byEntidad?entidadId=${dni}&tipoEntidad=3`);
                const archivoText = await archivoRes.text();

                if (!archivoRes.ok) throw new Error(`Error al obtener archivo: ${archivoText}`);

                const archivoData = JSON.parse(archivoText);
                if (!Array.isArray(archivoData) || archivoData.length === 0) {
                    throw new Error("No se encontraron archivos asociados a este usuario.");
                }
                setArchivo(archivoData[0]);

                const usuarioRes = await fetch(`http://localhost:5000/api/Usuario/byDNI?dni=${dni}`);
                const usuarioData = await usuarioRes.json();

                if (!usuarioRes.ok) throw new Error(`Error al obtener usuario: ${JSON.stringify(usuarioData)}`);

                setUsuario(usuarioData);
            } catch (err) {
                console.error(err);
                setError(err.message);
            }
        };

        fetchDatos();
    }, [dni]);

    if (error) return <p style={{ color: 'red' }}>Error: {error}</p>;
    if (!archivo || !usuario) return <p>Cargando información del usuario...</p>;

    const base64Valida = archivo.archivoBase64?.length > 100;

    return (
        <div style={{ padding: '2rem' }}>
            <h2>Detalle del Usuario</h2>

            <div style={{ marginBottom: '2rem' }}>
                <p><strong>Nombre:</strong> {usuario.nombre} {usuario.apellido}</p>
                <p><strong>DNI:</strong> {usuario.dni}</p>
                <p><strong>Email:</strong> {usuario.email}</p>
                <p><strong>Teléfono:</strong> {usuario.telefono}</p>
                <p><strong>Dirección:</strong> {usuario.calle} {usuario.altura} {usuario.piso && `Piso ${usuario.piso}`} {usuario.dpto && `Dpto ${usuario.dpto}`}</p>
                <p><strong>Fecha de Nacimiento:</strong> {new Date(usuario.fecNacimiento).toLocaleDateString()}</p>
            </div>

            <div>
                <p><strong>Nombre del archivo:</strong> {archivo.nombre}</p>
                <p><strong>Descripción:</strong> {archivo.descripcion}</p>

                {base64Valida ? (
                    <img
                        src={`data:image/jpeg;base64,${archivo.archivoBase64}`}
                        alt="DNI del usuario"
                        style={{ maxWidth: '400px', border: '1px solid #ccc', marginTop: '1rem' }}
                        onError={(e) => {
                            e.target.src = '';
                            console.error("Error cargando imagen base64");
                        }}
                    />
                ) : (
                    <p>No se pudo cargar la imagen.</p>
                )}
            </div>

            <div style={{ marginTop: '2rem', display: 'flex', gap: '1rem' }}>
                <button type="button" onClick={handClick}>Aceptar solicitud</button>
                <button type="button" onClick={handleRechazar} style={{ backgroundColor: 'crimson', color: 'white' }}>
                    Rechazar solicitud
                </button>
            </div>
        </div>
    );
};

export default DetalleUsuario;
