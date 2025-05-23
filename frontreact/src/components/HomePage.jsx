import Maquinaria from "./Maquinaria";
import React, { useEffect, useState } from "react";
import './HomePage.css';
import logo from '../assets/bobElAlquiladorLogoCompleto.svg';



function HomePage() {
  const [maquinarias, setMaquinarias] = useState([]);
  const [busqueda, setBusqueda] = useState('');

  useEffect(() => {
    fetch('http://localhost:3001/maquinarias')
      .then(response => {
        if (!response.ok) {
          throw new Error('Error en la respuesta');
        }
        return response.json();
      })
      .then(data => {  
        setMaquinarias(data);
      })
      .catch(error => {
        console.error('Error al obtener maquinarias:', error);
      });
  }, []);

  return (
    <div className="homepage-wrapper">
      <header className="homepage-header">
        <img src={logo} alt="Bob el alquilador" style={{width:'200px'}} />
      </header>

      <div className="homepage-container">
        <aside className="filter-sidebar">
          <input
            type="text"
            placeholder="Buscar por nombre o modelo"
            value={busqueda}
            onChange={(e) => setBusqueda(e.target.value)}
          />

          <h3>Filtrar</h3>
          <select>
            <option>Ubicación</option>
            <option>Buenos Aires</option>
            <option>Córdoba</option>
            <option>Mendoza</option>
          </select>
          <select>
            <option>Tipo de maquinaria</option>
            <option>Excavadora</option>
            <option>Grua</option>
            <option>Camión</option>
          </select>
          <select>
            <option>Marca</option>
            <option>CAT</option>
            <option>John Deere</option>
            <option>Komatsu</option>
          </select><CiShoppingTag /> 
          <select>
            <option>Permiso</option>
            <option>Requiere</option>
            <option>No requiere</option>
          </select><input type="text"  placeholder="Modelo" />
          <input type="number" placeholder="Precio máximo" />
          <button>Aplicar filtros</button>
        </aside>

        <main className="homepage-main">
          <h2 className="homepage-title">Publicaciones más recientes</h2>
          <div className="maquinarias-grid">
            {maquinarias
                .filter((m) =>
                  m.nombre?.toLowerCase().includes(busqueda.toLowerCase()) ||
                  m.modelo?.toLowerCase().includes(busqueda.toLowerCase())
                )
                .map((m) => (
                  <Maquinaria key={m.id} maquinaria={m} />
              ))}

          </div>
        </main>
      </div>
    </div>
  );
}

export default HomePage;
