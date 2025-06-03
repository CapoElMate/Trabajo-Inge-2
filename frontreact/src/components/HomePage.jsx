import axios from 'axios';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import SidebarFilters from './SidebarFilters';

function HomePage() {
    const [publicaciones, setPubs] = useState([]);
    const [imagenes, setImagenes] = useState({});
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [filtradas, setFiltradas] = useState([]);
    const [mensaje, setMensaje] = useState('');
    const navigate = useNavigate();

    const handleFiltrar = (filtros) => {
        if (parseFloat(filtros.precioMin) > parseFloat(filtros.precioMax)) {
            setMensaje('El precio por día mínimo debe ser menor al precio por día máximo');
            setFiltradas([]);
            return;
        }

        const resultado = publicaciones.filter(pub => {
            const tipo = pub.maquina?.tipoMaquina?.tipo?.toLowerCase() || '';
            const marca = pub.maquina?.modelo?.marca?.marca?.toLowerCase() || '';
            const modelo = pub.maquina?.modelo?.modelo?.toLowerCase() || '';
            const precio = pub.precioPorDia || 0;
            const ubicacion = pub.ubicacion?.ubicacionName?.toLowerCase() || '';

            return (!filtros.tipo || tipo.includes(filtros.tipo.toLowerCase())) &&
                (!filtros.marca || marca.includes(filtros.marca.toLowerCase())) &&
                (!filtros.modelo || modelo.includes(filtros.modelo.toLowerCase())) &&
                (!filtros.precioMin || precio >= parseFloat(filtros.precioMin)) &&
                (!filtros.precioMax || precio <= parseFloat(filtros.precioMax)) &&
                (!filtros.ubicacion || ubicacion.includes(filtros.ubicacion.toLowerCase()));
        });

        if (resultado.length === 0) {
            setMensaje('No se encontraron publicaciones para los filtros aplicados.');
        } else {
            setMensaje('');
        }

        setFiltradas(resultado);
    };

    useEffect(() => {
        setLoading(true);
        setError(null);

        axios.get('http://localhost:5000/api/Publicacion/all')
            .then(response => {
                const fetchedPublicaciones = response.data;
                setPubs(fetchedPublicaciones);

                const imagePromises = fetchedPublicaciones.map(pub =>
                    axios.get(`http://localhost:5000/api/Archivo/byEntidad?entidadId=${pub.idPublicacion}&tipoEntidad=0`)
                        .then(res => ({
                            id: pub.idPublicacion,
                            imageData: res.data[0]
                        }))
                        .catch(() => ({ id: pub.idPublicacion, imageData: null }))
                );

                Promise.all(imagePromises)
                    .then(results => {
                        const newImages = results.reduce((acc, current) => {
                            if (current.imageData) {
                                acc[current.id] = current.imageData;
                            }
                            return acc;
                        }, {});
                        setImagenes(newImages);
                    })
                    .finally(() => setLoading(false));
            })
            .catch(err => {
                console.error('Error cargando publicaciones:', err);
                setError('No se pudieron cargar las publicaciones. Por favor, inténtalo más tarde.');
                setLoading(false);
            });
    }, []);

    const handlePublicacionClick = (id) => {
        navigate(`/detallePublicacion/${id}`);
    };

    if (loading) return <div style={{ textAlign: 'center', padding: '20px' }}>Cargando publicaciones...</div>;
    if (error) return <div style={{ textAlign: 'center', padding: '20px', color: 'red' }}>{error}</div>;

    const publicacionesParaMostrar = filtradas.length > 0 || mensaje ? filtradas : publicaciones;

    return (
        <>
            <Header />
            <h1>Publicaciones Disponibles</h1>
            <div style={{ display: 'flex' }}>
                <SidebarFilters onFiltrar={handleFiltrar} />
                <div style={{ flex: 1, padding: '20px' }}>
                    {mensaje && <p>{mensaje}</p>}
                    <div style={{
                        display: 'grid',
                        gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))',
                        gap: '20px'
                    }}>
                        {publicacionesParaMostrar.map(pub => (
                            <div
                                key={pub.idPublicacion}
                                onClick={() => handlePublicacionClick(pub.idPublicacion)}
                                style={{
                                    border: '1px solid #e0e0e0',
                                    borderRadius: '8px',
                                    overflow: 'hidden',
                                    boxShadow: '0 4px 8px rgba(0,0,0,0.1)',
                                    cursor: 'pointer',
                                    transition: 'transform 0.2s ease-in-out',
                                    display: 'flex',
                                    flexDirection: 'column',
                                    justifyContent: 'space-between'
                                }}
                                onMouseOver={e => e.currentTarget.style.transform = 'translateY(-5px)'}
                                onMouseOut={e => e.currentTarget.style.transform = 'translateY(0)'}
                            >
                                {imagenes[pub.idPublicacion] ? (
                                    <img
                                        src={`data:image/jpeg;base64,${imagenes[pub.idPublicacion]}`}
                                        alt="Máquina"
                                        style={{ width: '100%', height: '200px', objectFit: 'cover' }}
                                    />
                                ) : (
                                    <div style={{
                                        width: '100%',
                                        height: '200px',
                                        backgroundColor: '#f0f0f0',
                                        display: 'flex',
                                        alignItems: 'center',
                                        justifyContent: 'center'
                                    }}>
                                        <p>Cargando imagen...</p>
                                    </div>
                                )}
                                <div style={{ padding: '15px' }}>
                                    <h3 style={{ margin: '0 0 10px 0', fontSize: '1.2em' }}>
                                        {pub.maquina?.modelo?.marca?.marca} {pub.maquina?.modelo?.modelo}
                                    </h3>
                                    <p style={{ margin: '5px 0', fontSize: '1.5em', fontWeight: 'bold', color: '#cc0000' }}>
                                        ${pub.precioPorDia} / Día
                                    </p>
                                    <p style={{ margin: '5px 0', color: '#555', fontSize: '0.9em' }}>
                                        Ubicación: {pub.ubicacion?.ubicacionName || 'N/A'}
                                    </p>
                                    {pub.maquina?.anioFabricacion && (
                                        <p style={{ margin: '5px 0', color: '#555', fontSize: '0.9em' }}>
                                            Año: {pub.maquina.anioFabricacion}
                                        </p>
                                    )}
                                    {pub.maquina?.tipoMaquina?.tipo && (
                                        <p style={{ margin: '5px 0', color: '#555', fontSize: '0.9em' }}>
                                            Tipo: {pub.maquina.tipoMaquina.tipo}
                                        </p>
                                    )}
                                    <p style={{
                                        margin: '10px 0 0 0',
                                        color: '#777',
                                        fontSize: '0.85em',
                                        maxHeight: '60px',
                                        overflow: 'hidden',
                                        textOverflow: 'ellipsis'
                                    }}>
                                        {pub.descripcion}
                                    </p>
                                </div>
                            </div>
                        ))}
                    </div>
                </div>
            </div>
        </>
    );
}

export default HomePage;
