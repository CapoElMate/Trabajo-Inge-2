import React, { useEffect, useState } from "react";
import "./Perfil.css";
import Header from "../Header";
import { useNavigate } from "react-router-dom";
import { useAuth } from "../../AuthContext";

export default function PerfilCliente() {
  const [cliente, setCliente] = useState(null);
  const [reservas, setReservas] = useState([]);
  const [alquileres, setAlquileres] = useState([]);
  const [reembolsos, setReembolsos] = useState([]);
  const [permisoEspeciales, setPermisoEspeciales] = useState([]);
  const [loading, setLoading] = useState(true);
  const { user, loadAuth } = useAuth();
  // const dniCliente = "2050022";
  const navigate = useNavigate();

  useEffect(() => {
    const fetchDatos = async () => {
      try {
        const getDNI = await fetch(
          `http://localhost:5000/api/Usuario/byEmail?email=${user?.userName}`
        )
          .then((res) => res.json())
          .then((data) => data.dni);

        const [
          resCliente,
          resReservas,
          resAlquileres,
          resReembolsos,
          resPermisosEspeciales,
        ] = await Promise.all([
          fetch(`http://localhost:5000/api/Cliente/byDNI?DNI=${getDNI}`),
          fetch(`http://localhost:5000/api/Reserva/byDNI?DNI=${getDNI}`),
          fetch(`http://localhost:5000/api/Alquiler/byDNI?dni=${getDNI}`),
          fetch(`http://localhost:5000/api/Reembolso/byDNI?dni=${getDNI}`),
          fetch(
            "http://localhost:5000/api/PermisosEspeciales/AllPermisosUsuarios"
          ),
        ]);

        const dataCliente = await resCliente.json();
        const dataReservas = await resReservas.json();
        const dataAlquileres = await resAlquileres.json();
        const dataReembolsos = await resReembolsos.json();
        const dataPermisosEspeciales = await resPermisosEspeciales.json();

        const permisosFiltrados = dataPermisosEspeciales.filter(
          (permiso) => permiso.dniCliente === getDNI
        );

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

        const ReembolsoMasPublicacion = await Promise.all(
          dataReembolsos.map(async (reembolso) => {
            // Buscar el alquiler que corresponde a este reembolso (suponiendo que hay un idAlquiler)
            const alquilerRelacionado = dataAlquileres.find(
              (alquiler) => alquiler.idAlquiler === reembolso.idAlquiler
            );

            // Si existe el alquiler, obtenemos el idPublicacion desde alquiler.reserva.idPublicacion
            const idPublicacion = alquilerRelacionado?.reserva?.idPublicacion;

            let publicacion = null;
            if (idPublicacion) {
              const res = await fetch(
                `http://localhost:5000/api/Publicacion/byId?id=${idPublicacion}`
              );
              publicacion = await res.json();
            }

            // Devuelvo reembolso + la publicación + el alquiler relacionado si quieres
            return { ...reembolso, publicacion };
          })
        );

        setCliente(dataCliente.usuarioRegistrado);
        setReservas(reservasMasPublicacion);
        setAlquileres(alquiilerMasPublicacion);
        setReembolsos(ReembolsoMasPublicacion);
        setPermisoEspeciales(permisosFiltrados);
      } catch (error) {
        console.error("Error al cargar los datos del perfil:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchDatos();
  }, [loadAuth, user]);

  if (loading && loadAuth)
    return <p className="text-center">Cargando perfil...</p>;

  return (
    <div>
      <Header />
      <div className="profile-container">
        <h1>Datos del cliente</h1>

        {cliente && (
          <div>
            <div className="personalInformacion">
              <p>
                <strong>Cliente:</strong> {cliente.nombre} {cliente.apellido}
              </p>
              <p>
                <strong>DNI:</strong> {cliente.dni}
              </p>
              <p>
                <strong>Email:</strong> {cliente.email}
              </p>
            </div>

            {permisoEspeciales.length > 0 && (
              <div className="permisos-especiales">
                <h4>Permisos Especiales</h4>
                {permisoEspeciales.map((permiso, index) => (
                  <div key={index} className="permiso-card">
                    <span>
                      <strong>Permiso:</strong> {permiso.permiso}
                    </span>
                    <span>
                      <strong>Vigencia:</strong>{" "}
                      {new Date(permiso.fecEmision).toLocaleDateString()} a{" "}
                      {new Date(permiso.fecVencimiento).toLocaleDateString()}
                    </span>
                    <span>
                      <strong>Estado:</strong> {permiso.status}
                    </span>
                  </div>
                ))}
              </div>
            )}
          </div>
        )}

        <div className="user-info-card">
          <h2 className="reservas-title">Reservas pendientes de efectivizar</h2>
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
            <p className="reserva-empty">
              No hay reservas pendientes de efectivizar.
            </p>
          )}
        </div>

        <div className="user-info-card">
          <h2 className="reservas-title">Alquileres activos</h2>
          {alquileres.filter((a) => a.status === "Efectivizado").length > 0 ? (
            <div className="reservas-list">
              {alquileres
                .filter((a) => a.status === "Efectivizado")
                .map((alquiler) => (
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
            <p className="reserva-empty">No hay alquileres activos.</p>
          )}
        </div>

        <div className="user-info-card">
          <h2 className="reservas-title">Reembolsos pendientes</h2>
          {reembolsos.length > 0 ? (
            <div className="reservas-list">
              {reembolsos.map((reembolso) => (
                <div
                  key={reembolso.idReembolso}
                  className="reserva-card reembolso-card"
                >
                  <p>
                    <span className="reserva-label">Maquina:</span>{" "}
                    {reembolso.publicacion.maquina.modelo.marca.marca}{" "}
                    {reembolso.publicacion.maquina.modelo.modelo}
                  </p>
                  <p>
                    <span className="reserva-label">Motivo:</span>{" "}
                    {reembolso.motivo}
                  </p>
                  <p>
                    <span className="reserva-label">Monto:</span> $
                    {reembolso.monto}
                  </p>
                  <p>
                    <span className="reserva-label">Estado: </span>
                    {reembolso.status}
                  </p>
                </div>
              ))}
            </div>
          ) : (
            <p className="reserva-empty">No hay reembolsos registrados.</p>
          )}
        </div>
      </div>
    </div>
  );
}
