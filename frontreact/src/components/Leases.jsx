import React, { useEffect, useState } from 'react';
import Header from './Header';
import Filters from './FIlters';
import './Leases.css';
function Leases() {
  const [alquileres, setAlquileres] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch('http://localhost:3001/alquileres')
      .then(response => {
        if (!response.ok) {
          throw new Error('Error al obtener alquileres');
        }
        return response.json();
      })
      .then(data => {
        setAlquileres(data);
        setLoading(false);
      })
      .catch(error => {
        console.error('Error:', error);
        setLoading(false);
      });
  }, []);

  if (loading) return <p>Cargando alquileres...</p>;

  return (
    
    <div>
      <Header/>
      <div  style={{display:'flex'}}>
      <Filters/>
      <div>
        <h2>Lista de Alquileres</h2>
      <div className='leases-container'>
        {alquileres.map((a, i) => (
          <div key={i} className='lease-info'>
            <p>Maquinaria : <strong>{a.Maquinaria}</strong></p>
            <p>Fecha de vencimiento  : {a.fechaVencimiento}</p>
            <p> Precio : {a.precio}</p>
          </div>
        ))}
        </div>
      </div>
      </div>
    </div>
  );
};

export default Leases;
