import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import "./ReservasPendientes.css";
import Header from "./Header";
import { useAuth } from "../AuthContext";

const ReservasPendientes = () => {
  const [reservas, setReservas] = useState([]);
  const [filtradas, setFiltradas] = useState([]);
  const {user,loading} = useAuth();
  const [filtros, setFiltros] = useState({
    fechaInicio: "",
    fechaFin: "",
    tipoMaquinaria: "",
    dniCliente: ""
  });
  
  const navigate = useNavigate();
  
  useEffect(() => {
    const fetchReservas = async () => {
      try {
        const res = await fetch("http://localhost:5000/api/reserva/all");
        const data = await res.json();
        setReservas(data);
        setFiltradas(data);
      } catch (error) {
        console.error("Error al obtener reservas:", error);
      }
    };
    fetchReservas();
  }, []);

  const handleChange = (e) => {
    setFiltros({ ...filtros, [e.target.name]: e.target.value });
  };

  const filtrarReservas = () => {
    let resultado = [...reservas];

    if (filtros.fechaInicio) {
      resultado = resultado.filter(r => new Date(r.fecInicio) >= new Date(filtros.fechaInicio));
    }
    if (filtros.fechaFin) {
      resultado = resultado.filter(r => new Date(r.fecFin) <= new Date(filtros.fechaFin));
    }
    if (filtros.tipoMaquinaria) {
      resultado = resultado.filter(r => r.tipoEntrega?.entrega.toLowerCase().includes(filtros.tipoMaquinaria.toLowerCase()));
    }
    if (filtros.dniCliente) {
      resultado = resultado.filter(r => r.dniCliente === filtros.dniCliente);
    }

    setFiltradas(resultado);
  };
  if(loading){
    return <p>Cargando....</p>
  }
  return (
    <>
    <Header/>
    <div className="reservas-container">
      {/* Panel de Filtros */}
      <div className="filtros-panel">
        <h2>Filtrar Reservas</h2>
        <div className="filtro-item">
          <label>Fecha Inicio:</label>
          <input type="date" name="fechaInicio" value={filtros.fechaInicio} onChange={handleChange} />
        </div>
        <div className="filtro-item">
          <label>Fecha Fin:</label>
          <input type="date" name="fechaFin" value={filtros.fechaFin} onChange={handleChange} />
        </div>
        <div className="filtro-item">
          <label>Tipo de Maquinaria:</label>
          <input type="text" name="tipoMaquinaria" value={filtros.tipoMaquinaria} onChange={handleChange} />
        </div>
        <div className="filtro-item">
          <label>DNI Cliente:</label>
          <input type="text" name="dniCliente" value={filtros.dniCliente} onChange={handleChange} />
        </div>
        {console.log(user)}
        {user.roles.includes("Cliente")?(
              <button className="filtrar-btn" onClick={filtrarReservas}>Filtrar</button>
        ):("")}
      </div>

      {/* Panel de Resultados */}
      <div className="resultados-panel">
        {filtradas.length === 0 ? (
          <p>No hay reservas que coincidan con los filtros.</p>
        ) : (
          filtradas.map(reserva => (
            <div
              key={reserva.idReserva}
              className="reserva-card"
              onClick={() => navigate(`/DetalleReserva/${reserva.idReserva}`)}>
              <h3>Reserva #{reserva.idReserva}</h3>
              <p>Inicio: {new Date(reserva.fecInicio).toLocaleDateString()}</p>
              <p>Fin: {new Date(reserva.fecFin).toLocaleDateString()}</p>
              <p>Status: {reserva.status}</p>
              <p>Dirección: {reserva.calle} {reserva.altura}, {reserva.dpto}</p>
              <p>Tipo Entrega: {reserva.tipoEntrega?.entrega}</p>
              <p>DNI Cliente: {reserva.dniCliente}</p>
            </div>
          ))
        )}
      </div>
    </div>
 </> );
};

export default ReservasPendientes;
