// detalleUsuario.jsx
import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';

const DetalleUsuario = () => {
    const { dni } = useParams();
    const [archivo, setArchivo] = useState(null);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchArchivo = async () => {
            try {
                const res = await fetch(`http://localhost:5000/api/Archivo/byEntidad?entidadId=${dni}&tipoEntidad=3`);
                if (!res.ok) throw new Error(`Error al obtener archivo: ${await res.text()}`);
                const data = await res.json();
                setArchivo(data);
            } catch (err) {
                console.error(err);
                setError(err.message);
            }
        };

        fetchArchivo();
    }, [dni]);

    if (error) return <p>Error: {error}</p>;
    if (!archivo) return <p>Cargando archivo...</p>;

    const base64Valida = archivo.archivoBase64?.length > 100; // Valor razonable para evitar errores

    return (
        <div style={{ padding: '2rem' }}>
            <h2>Detalle del Usuario</h2>
            <p><strong>Nombre:</strong> {archivo.nombre}</p>
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
    );
};

export default DetalleUsuario;
