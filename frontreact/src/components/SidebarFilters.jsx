import { useEffect, useState } from 'react';

// Opciones para los filtros
const opcionesTipo = ['Transporte', 'Construccion', 'Agricola'];
const opcionesMarca = ['John Deere', 'Case IH', 'New Holland'];
const opcionesModelo = ['Serie A', 'Serie B', 'Serie C'];
const opcionesUbicacion = ['La Plata', 'Quilmes', 'Beriso'];

function SidebarFilters({ onFiltrar }) {
  const [filtros, setFiltros] = useState({
    tipo: '',
    marca: '',
    modelo: '',
    precioMin: '',
    precioMax: '',
    ubicacion: ''
  });

  const [tipos, setTipos] = useState([]);
  const [marcas, setMarcas] = useState([]);
  const [modelos, setModelos] = useState([]);
  const [ubicaciones] = useState(['La Plata', 'Quilmes', 'Beriso']); // Si querés, esto también puede venir de la API

  // Obtener tipos de máquinas y marcas al iniciar
  useEffect(() => {
    fetch("http://localhost:5000/api/Maquinas/all")
      .then((res) => res.json())
      .then((data) => {
        const tiposUnicos = [...new Set(data.map((m) => m.tipoMaquina.tipo))];
        setTipos(tiposUnicos);
      });

    getMarcas().then(setMarcas);
  }, []);

  // Actualizar modelos al cambiar la marca
  useEffect(() => {
    if (filtros.marca) {
      getModelos(filtros.marca).then(setModelos);
    } else {
      setModelos([]);
    }
  }, [filtros.marca]);

  const getMarcas = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Marcas/all");
      const data = await response.json();
      return data.map((m) => m.marca);
    } catch (error) {
      console.error("Error al obtener las marcas", error);
      return [];
    }
  };

  const getModelos = async (marca) => {
    try {
      const response = await fetch("http://localhost:5000/api/Modelos/all");
      const data = await response.json();
      return data.filter((m) => m.marca.marca === marca).map((m) => m.modelo);
    } catch (error) {
      console.error("Error al obtener los modelos", error);
      return [];
    }
  };

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
            {tipos.map(tipo => <option key={tipo} value={tipo}>{tipo}</option>)}
          </select>
        </label>

        <label>
          Marca
          <select name="marca" value={filtros.marca} onChange={handleChange}>
            <option value="">Todas</option>
            {marcas.map(marca => <option key={marca} value={marca}>{marca}</option>)}
          </select>
        </label>

        <label>
          Modelo
          <select name="modelo" value={filtros.modelo} onChange={handleChange} disabled={!filtros.marca}>
            <option value="">Todos</option>
            {modelos.map(modelo => <option key={modelo} value={modelo}>{modelo}</option>)}
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
            {ubicaciones.map(ubic => <option key={ubic} value={ubic}>{ubic}</option>)}
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