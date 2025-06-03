import React, { useEffect, useState } from "react";
import "./Perfil.css";
import Header from "../Header";
import { useNavigate } from "react-router-dom";

export default function PerfilCliente() {
  const [cliente, setCliente] = useState(null);
  const [reservas, setReservas] = useState([]);
  const [alquileres, setAlquileres] = useState([]);
  const [reembolsos, setReembolsos] = useState([]);
  const [loading, setLoading] = useState(true);

  const dniCliente = "2050022";
  const navigate = useNavigate();

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

        const reservasMasPublicacion = await Promise.all(
          dataReservas.map(async (reserva) => {
            // Traer la publicacion correspondiente a esta reserva
            const res = await fetch(
              `http://localhost:5000/api/Publicacion/byId?id=${reserva.idPublicacion}`
            );
            const publicacion = await res.json();

            // Combinar datos de reserva + publicacion (como "p" por ejemplo)
            return { ...reserva, publicacion };
          })
        );

        const alquiilerMasPublicacion = await Promise.all(
          dataAlquileres.map(async (alquiler) => {
            // Traer la publicacion correspondiente a esta reserva
            const res = await fetch(
              `http://localhost:5000/api/Publicacion/byId?id=${alquiler.reserva.idPublicacion}`
            );
            const publicacion = await res.json();

            // Combinar datos de reserva + publicacion (como "p" por ejemplo)
            return { ...alquiler, publicacion };
          })
        );


        setCliente(dataCliente.usuarioRegistrado);
        setReservas(reservasMasPublicacion);
        setAlquileres(alquiilerMasPublicacion);
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
          {reservas.filter((r) => r.status === "Lista para efectivizar")
            .length > 0 ? (
            <div className="reservas-list">
              {reservas
                .filter((r) => r.status === "Lista para efectivizar")
                .map((reserva) => (
                  <div
                    key={reserva.idReserva}
                    className="reserva-card"
                    onClick={() =>
                      navigate(`/DetalleReserva/${reserva.idReserva}`)
                    }
                  >
                    <p>
                      <span className="reserva-label">Titulo:</span>{" "}
                      {reserva.publicacion.titulo}
                    </p>
                    <p>
                      <span className="reserva-label">Maquina:</span>{" "}
                      {reserva.publicacion.maquina.modelo.marca.marca}{" "}
                      {reserva.publicacion.maquina.modelo.modelo}
                    </p>
                    <p>
                      <span className="reserva-label">
                        Click para más detalles
                      </span>
                    </p>
                  </div>
                ))}
            </div>
          ) : (
            <p className="reserva-empty">No hay reservas registradas.</p>
          )}
        </div>

        <div className="user-info-card">
          <h2 className="reservas-title">Alquileres</h2>
          {alquileres.length > 0 ? (
            <div className="reservas-list">
              {alquileres.map((alquiler) => (
                <div
                    key={alquiler.reserva.idReserva}
                    className="reserva-card"
                    onClick={() =>
                      navigate(`/DetalleAlquiler/${alquiler.idAlquiler}`)
                    }
                  >
                    <p>
                      <span className="reserva-label">Titulo:</span>{" "}
                      {alquiler.publicacion.titulo}
                    </p>
                    <p>
                      <span className="reserva-label">Maquina:</span>{" "}
                      {alquiler.publicacion.maquina.modelo.marca.marca}{" "}
                      {alquiler.publicacion.maquina.modelo.modelo}
                    </p>
                    <p>
                      <span className="reserva-label">
                        Click para más detalles
                      </span>
                    </p>
                  </div>
              ))}
            </div>
          ) : (
            <p className="reserva-empty">No hay alquileres registrados.</p>
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
