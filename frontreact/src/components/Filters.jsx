import './Filters.css';
import { useState } from 'react';
function Filters(){
     const [busqueda, setBusqueda] = useState('');
    return(<><aside className="filter-sidebar">
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
                </select> 
                <select>
                  <option>Permiso</option>
                  <option>Requiere</option>
                  <option>No requiere</option>
                </select><input type="text"  placeholder="Modelo" />
                <input type="number" placeholder="Precio máximo" />
                <button>Aplicar filtros</button>
              </aside></>);
}
export default Filters;