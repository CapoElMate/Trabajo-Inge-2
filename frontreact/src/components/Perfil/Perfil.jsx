import React, { useEffect, useState } from "react";
import "./Perfil.css";
import Header from "../Header";

export default function PerfilCliente() {
  const [cliente, setCliente] = useState(null);
  const [reservas, setReservas] = useState([]);
  const [alquileres, setAlquileres] = useState([]);
  const [reembolsos, setReembolsos] = useState([]);
  const [loading, setLoading] = useState(true);

  const dniCliente = "2050022";

  useEffect(() => {
    const fetchDatos = async () => {
      try {
        const [resCliente, resReservas, resAlquileres, resReembolsos] =
          await Promise.all([
            fetch(`http://localhost:5000/api/Cliente/byDNI?DNI=${dniCliente}`),
            fetch(`http://localhost:5000/api/Reserva/byDNI?DNI=${dniCliente}`),
            fetch(`http://localhost:5000/api/Alquiler/byDNI?dni=${dniCliente}`),
            fetch(
              `http://localhost:5000/api/Reembolso/byDNI?dni=${dniCliente}`
            ),
          ]);

        const dataCliente = await resCliente.json();
        const dataReservas = await resReservas.json();
        const dataAlquileres = await resAlquileres.json();
        const dataReembolsos = await resReembolsos.json();

        setCliente(dataCliente.usuarioRegistrado);
        setReservas(dataReservas);
        setAlquileres(dataAlquileres);
        setReembolsos(dataReembolsos);
      } catch (error) {
        console.error("Error al cargar los datos del perfil:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchDatos();
  }, []);

  if (loading) return <p className="text-center">Cargando perfil...</p>;

  return (
    <div>
      <Header />
      <div className="profile-container">
        <h1>Datos del cliente</h1>

        {cliente && (
          <div className="personalInformacion">
            <p>
              <strong>Cliente</strong> {cliente.nombre} {cliente.apellido}
            </p>
            <p>
              <strong>DNI</strong> {cliente.dni}
            </p>
            <p>
              <strong>Email</strong> {cliente.email}
            </p>
          </div>
        )}

<div className="user-info-card">
  <h2 className="reservas-title">Reservas</h2>
  {reservas.length > 0 ? (
    <div className="reservas-list">
      {reservas.map((reserva) => (
        <div
          key={reserva.idReserva}
          className="reserva-card"
          onClick={() => console.log("Click en reserva", reserva.idReserva)}
        >
          <p>
            <span className="reserva-label">Desde:</span>{" "}
            {reserva.fecInicio.split("T")[0]}
          </p>
          <p>
            <span className="reserva-label">Hasta:</span>{" "}
            {reserva.fecFin.split("T")[0]}
          </p>
          <p>
            <span className="reserva-label">Estado:</span>{" "}
            <em>{reserva.status}</em>
          </p>
        </div>
      ))}
    </div>
  ) : (
    <p className="reserva-empty">No hay reservas registradas.</p>
  )}
</div>

        <div className="user-info-card">
          <h2 className="text-xl font-semibold mb-2">Alquileres</h2>
          {alquileres.length > 0 ? (
            <ul className="list-disc pl-5">
              {alquileres.map((alquiler) => (
                <li key={alquiler.idAlquiler}>
                  Desde <strong>{alquiler.fecInicio}</strong> hasta{" "}
                  <strong>{alquiler.fecFin}</strong> - Vehículo:{" "}
                  {alquiler.patenteVehiculo}
                </li>
              ))}
            </ul>
          ) : (
            <p>No hay alquileres registrados.</p>
          )}
        </div>

        <div className="user-info-card">
          <h2 className="text-xl font-semibold mb-2">Reembolsos</h2>
          {reembolsos.length > 0 ? (
            <ul className="list-disc pl-5">
              {reembolsos.map((reembolso) => (
                <li key={reembolso.idReembolso}>
                  Monto: ${reembolso.monto} - Motivo: {reembolso.motivo}
                </li>
              ))}
            </ul>
          ) : (
            <p>No hay reembolsos registrados.</p>
          )}
        </div>
      </div>
    </div>
  );
}
