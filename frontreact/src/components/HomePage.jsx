import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import Header from './Header.jsx';
import Filters from './Filters.jsx';
import './HomePage.css'; 
import { useAuth } from '../AuthContext'; 
function HomePage() {
  const [publicaciones, setPublicaciones] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  const { user, logout } = useAuth(); 

  useEffect(() => {
    const fetchPublicaciones = async () => {
      try {
        const response = await fetch('http://localhost:3001/publicaciones');
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setPublicaciones(data);
      } catch (error) {
        console.error("Error fetching publications:", error);
        setError("No se pudieron cargar las publicaciones. Intenta de nuevo más tarde.");
      } finally {
        setLoading(false);
      }
    };

    fetchPublicaciones();
  }, []);
  let num = [publicaciones.length/6]
  const handleCardClick = (id) => {
    navigate(`/PostDetail/${id}`); // Redirect to the detail page
  };

  if (loading) {
    return <div className="home-page-container">Cargando publicaciones...</div>;
  }

  if (error) {
    return <div className="home-page-container error-message">{error}</div>;
  }

  return (
      <div className="home-page-container">
      <Header/>
      <div style={{display:'flex'}}>
      <Filters/>
      {publicaciones.length === 0 ? (
        <p>No hay publicaciones disponibles en este momento.</p>
      ) : (
        <div className="publicaciones-grid">
          {publicaciones.map((pub) => (
            <div
              key={pub.id}
              className={`publicacion-card ${!pub.disponible ? 'unavailable' : ''}`}
              onClick={() => handleCardClick(pub.id)}
            >
              <img
                src={pub.imagenes[0] || 'https://via.placeholder.com/400x300/CCCCCC/000000?text=No+Image'}
                alt={pub.nombreMaquina}
                className="publicacion-image"
              />
              <div className="publicacion-info">
                <h2>{pub.nombreMaquina}</h2>
                <p>
                  <strong>Precio por día:</strong> ${pub.precioPorDia.toLocaleString('es-AR')}
                </p>
                <p>
                  <strong>Ubicación:</strong> {pub.ubicacionActual.calle},{' '}
                  {pub.ubicacionActual.altura}
                  {pub.ubicacionActual.departamento && `, Dpto. ${pub.ubicacionActual.departamento}`}
                </p>
                {pub.tagsAdicionales && pub.tagsAdicionales.length > 0 && (
                  <p className="publicacion-tags">
                    <strong>Tags:</strong> {pub.tagsAdicionales.join(', ')}
                  </p>
                )}
                {!pub.disponible && <div className="unavailable-overlay">No Disponible</div>}
              </div>
            </div>
          ))}
          <div>
            {
              num.map((key)=>(
                  <button>{key}</button>
              ))
            }
          </div>
        </div>
      )}
    </div>
    </div>
  );
}

export default HomePage;