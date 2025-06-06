import { useEffect, useState } from 'react';

// import { useState, useEffect } from 'react';

// Las funciones para obtener datos desde la API
const getMarcas = async () => {
  try {
    const response = await fetch("http://localhost:5000/api/Marcas/all");
    if (!response.ok) throw new Error("Error al obtener las marcas");
    const data = await response.json();
    return data.map(m => m.marca);
  } catch (error) {
    console.error("Error fetching marcas:", error);
    return [];
  }
};

const getModelos = async (marca = "") => {
  if (!marca) return [];
  try {
    const response = await fetch("http://localhost:5000/api/Modelos/all");
    if (!response.ok) throw new Error("Error al obtener los modelos");
    const data = await response.json();
    return data.filter(m => m.marca.marca === marca).map(m => m.modelo);
  } catch (error) {
    console.error("Error fetching modelos:", error);
    return [];
  }
};

const getTipos = async () => {
  try {
    const response = await fetch("http://localhost:5000/api/TiposMaquina/all");
    if (!response.ok) throw new Error("Error al obtener los tipos");
    const data = await response.json();
    return data.map(t => t.tipo);
  } catch (error) {
    console.error("Error fetching tipos:", error);
    return [];
  }
};

const getUbicaciones = async () => {
  try {
    const response = await fetch("http://localhost:5000/api/Ubicacion/all");
    if (!response.ok) throw new Error("Error al obtener las ubicaciones");
    const data = await response.json();
    return data.map(u => u.ubicacionName);
  } catch (error) {
    console.error("Error fetching ubicaciones:", error);
    return [];
  }
};

function SidebarFilters({ onFiltrar }) {
  const [filtros, setFiltros] = useState({
    tipo: '',
    marca: '',
    modelo: '',
    precioMin: '',
    precioMax: '',
    ubicacion: ''
  });

  // Estados para opciones dinámicas
  const [opcionesTipo, setOpcionesTipo] = useState([]);
  const [opcionesMarca, setOpcionesMarca] = useState([]);
  const [opcionesModelo, setOpcionesModelo] = useState([]);
  const [opcionesUbicacion, setOpcionesUbicacion] = useState([]);

  useEffect(() => {
    // Cargar tipos, marcas y ubicaciones al montar el componente
    getTipos().then(setOpcionesTipo);
    getMarcas().then(setOpcionesMarca);
    getUbicaciones().then(setOpcionesUbicacion);
  }, []);

  useEffect(() => {
    // Cuando cambia la marca, obtener los modelos asociados
    if (filtros.marca) {
      getModelos(filtros.marca).then(setOpcionesModelo);
    } else {
      setOpcionesModelo([]);
      setFiltros(prev => ({ ...prev, modelo: '' })); // Limpiar modelo si no hay marca
    }
  }, [filtros.marca]);

  // const getMarcas = async () => {
  //   try {
  //     const response = await fetch("http://localhost:5000/api/Marcas/all");
  //     const data = await response.json();
  //     return data.map((m) => m.marca);
  //   } catch (error) {
  //     console.error("Error al obtener las marcas", error);
  //     return [];
  //   }
  // };

  // const getModelos = async (marca) => {
  //   try {
  //     const response = await fetch("http://localhost:5000/api/Modelos/all");
  //     const data = await response.json();
  //     return data.filter((m) => m.marca.marca === marca).map((m) => m.modelo);
  //   } catch (error) {
  //     console.error("Error al obtener los modelos", error);
  //     return [];
  //   }
  // };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFiltros(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onFiltrar(filtros);
  };

 

  return (
    <aside style={{ width: '250px', padding: '20px', borderRight: '1px solid #ddd', backgroundColor: '#fafafa' }}>
      <h3>Filtrar</h3>
      <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>

        <label>
          Tipo de máquina
          <select name="tipo" value={filtros.tipo} onChange={handleChange}>
            <option value="">Todos</option>
{opcionesTipo.map(op => (
  <option key={op} value={op}>{op}</option>
))}
          </select>
        </label>

        <label>
          Marca
          <select name="marca" value={filtros.marca} onChange={handleChange}>
            <option value="">Todas</option>
{opcionesMarca.map(op => (
  <option key={op} value={op}>{op}</option>
))}
          </select>
        </label>

        <label>
          Modelo
          <select name="modelo" value={filtros.modelo} onChange={handleChange} disabled={!filtros.marca}>
  <option value="">Todos</option>
  {opcionesModelo.map(op => (
    <option key={op} value={op}>{op}</option>
  ))}
          </select>
        </label>

        <label>
          Precio mínimo
          <input
            type="number"
            name="precioMin"
            value={filtros.precioMin}
            onChange={handleChange}
            placeholder="Ej: 10000"
          />
        </label>

        <label>
          Precio máximo
          <input
            type="number"
            name="precioMax"
            value={filtros.precioMax}
            onChange={handleChange}
            placeholder="Ej: 500000"
          />
        </label>

        <label>
          Ubicación
          <select name="ubicacion" value={filtros.ubicacion} onChange={handleChange}>
            <option value="">Todas</option>
{opcionesUbicacion.map(op => (
  <option key={op} value={op}>{op}</option>
))}
          </select>
        </label>

        <button type="submit" style={{ backgroundColor: '#cc0000', color: 'white', padding: '10px', border: 'none' }}>
          Filtrar
        </button>
        
      </form>
    </aside>
  );
}

export default SidebarFilters;