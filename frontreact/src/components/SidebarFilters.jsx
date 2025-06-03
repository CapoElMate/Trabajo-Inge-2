import { useState } from 'react';

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

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFiltros(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onFiltrar(filtros);
  };

  const handleReset = () => {
    const filtrosVacios = {
      tipo: '',
      marca: '',
      modelo: '',
      precioMin: '',
      precioMax: '',
      ubicacion: ''
    };
    setFiltros(filtrosVacios);
    onFiltrar(filtrosVacios);
  };

  return (
    <aside style={{ width: '250px', padding: '20px', borderRight: '1px solid #ddd', backgroundColor: '#fafafa' }}>
      <h3>Filtrar</h3>
      <form onSubmit={handleSubmit} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>

        <label>
          Tipo de máquina
          <select name="tipo" value={filtros.tipo} onChange={handleChange}>
            <option value="">Todos</option>
            {opcionesTipo.map(op => <option key={op} value={op}>{op}</option>)}
          </select>
        </label>

        <label>
          Marca
          <select name="marca" value={filtros.marca} onChange={handleChange}>
            <option value="">Todas</option>
            {opcionesMarca.map(op => <option key={op} value={op}>{op}</option>)}
          </select>
        </label>

        <label>
          Modelo
          <select name="modelo" value={filtros.modelo} onChange={handleChange}>
            <option value="">Todos</option>
            {opcionesModelo.map(op => <option key={op} value={op}>{op}</option>)}
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
            {opcionesUbicacion.map(op => <option key={op} value={op}>{op}</option>)}
          </select>
        </label>

        <button type="submit" style={{ backgroundColor: '#cc0000', color: 'white', padding: '10px', border: 'none' }}>
          Filtrar
        </button>
        <button type="button" onClick={handleReset} style={{ padding: '10px', border: '1px solid #ccc' }}>
          Limpiar filtros
        </button>
      </form>
    </aside>
  );
}

export default SidebarFilters;
