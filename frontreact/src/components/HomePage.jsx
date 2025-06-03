import axios from 'axios';
import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header';
import logo from "../assets/bobElAlquiladorLogoCompleto.svg";

function HomePage() {
    const [publicaciones, setPubs] = useState([]);
    const [imagenes, setImagenes] = useState({});
    const [loading, setLoading] = useState(true); 
    const [error, setError] = useState(null);  
    const navigate = useNavigate();

    useEffect(() => {
        setLoading(true);
        setError(null);  

        axios.get('http://localhost:5000/api/Publicacion/all')
            .then(response => {
                const fetchedPublicaciones = response.data;
                setPubs(fetchedPublicaciones);

                if (fetchedPublicaciones.length === 0) {
                    setLoading(false);
                    return;
                }

                // Create an array of promises for image fetching
                const imagePromises = fetchedPublicaciones.map(pub =>
                    axios.get(`http://localhost:5000/api/Archivo/byEntidad?entidadId=${pub.idPublicacion}&tipoEntidad=0`)
                        .then(res => ({
                            id: pub.idPublicacion,
                            imageData: res.data[0] ///si muchas imagenes se agarra la primera
                        }))
                        .catch(err => {
                            console.error(`Error obteniendo imagen para pub ${pub.idPublicacion}:`, err);
                            return { id: pub.idPublicacion, imageData: null }; 
                        })
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
                    .finally(() => {
                        setLoading(false); 
                    });
            })
            .catch(err => {
                console.error('Error en petición GET de publicaciones:', err);
                setError('No se pudieron cargar las publicaciones. Por favor, inténtalo de nuevo más tarde.'); // Set an error message
                setLoading(false); 
            });
    }, []);

    const handlePublicacionClick = (idPublicacion) => {
        navigate(`/detallePublicacion/${idPublicacion}`);
    };
    if (loading) {
        return <div style={{ textAlign: 'center', padding: '20px' }}>Cargando publicaciones...</div>;
    }

    if (error) {
        return <div style={{ textAlign: 'center', padding: '20px', color: 'red' }}>Error: {error}</div>;
    }

    return (
        <>
          <Header/>
            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(300px, 1fr))', gap: '20px', padding: '20px' }}>
                {publicaciones.length > 0 ? (
                    publicaciones.map(pub => (
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
                                    alt={`Imagen de ${pub.maquina?.modelo?.modelo || 'máquina'}`}
                                    style={{ width: '100%', height: '200px', objectFit: 'cover' }}
                                />
                            ) : (
                                <div style={{ width: '100%', height: '200px', backgroundColor: '#f0f0f0', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
                                   <img src={logo} alt="Vista previa" className="preview-img" />
                                </div>
                            )}

                            <div style={{ padding: '15px' }}>
                                <h3 style={{ margin: '0 0 10px 0', fontSize: '1.2em' }}>
                                    {/* {pub.maquina?.modelo?.marca?.marca} {pub.maquina?.modelo?.modelo} */}
                                    {pub.titulo}
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

                                <p style={{ margin: '10px 0 0 0', color: '#777', fontSize: '0.85em', maxHeight: '60px', overflow: 'hidden', textOverflow: 'ellipsis' }}>
                                    {pub.descripcion}
                                </p>
                            </div>
                        </div>
                    ))
                ) : (
                     <p style={{ textAlign: 'center', width: '100%' }}>No hay publicaciones disponibles en este momento.</p>
                )}
            </div>
        </>
    );
}

export default HomePage;
