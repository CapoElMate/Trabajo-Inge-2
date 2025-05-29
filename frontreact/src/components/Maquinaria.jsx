import React from 'react';
import {useNavigate } from 'react-router-dom';

function Maquinaria({ maquinaria }) {
  const { nombre, descripcion, precio, ubicacion, imagen } = maquinaria;
const navigate=useNavigate(); 
    function redireccion(maquinaria){
        console.log(maquinaria.id)
        navigate(`/DetallePublicacion/${maquinaria.id}`);

    }
  return (
    <div className="maquinaria-card" onClick={() => redireccion(maquinaria)}>
      <img src={imagen} alt={nombre} className="maquinaria-img" />
      <div className="maquinaria-info">
        <h3>{nombre}</h3>
        <p><strong>Ubicación:</strong> {ubicacion}</p>
        <p className="precio">Precio por día: ${precio}</p>
        <button className="alquilar-btn">Alquilar <MdOutlineShoppingCartCheckout />
</button>
      </div>
    </div>
  );
}

export default Maquinaria;
