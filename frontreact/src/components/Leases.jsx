import React, { useState, useEffect } from 'react';
import './Leases.css';
import { useAuth } from '../AuthContext'; // Corrected import path for AuthContext
import Header from './Header'; // Assuming you want to keep the Header

function Leases() {
  const { user } = useAuth();
  const [leases, setLeases] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchLeases = async () => {
      if (!user || !user.dni) {
        setLoading(false);
        setError("No se pudo cargar el DNI del usuario para buscar los arriendos.");
        return;
      }

      try {
        // Fetch all leases first
        const response = await fetch('http://localhost:3001/leases');
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();

        // Filter leases by the current user's DNI
        const userLeases = data.filter(lease => lease.clientDni === user.dni);
        setLeases(userLeases);

      } catch (err) {
        console.error("Error fetching leases:", err);
        setError("No se pudieron cargar tus alquileres. Intenta de nuevo más tarde.");
      } finally {
        setLoading(false);
      }
    };

    fetchLeases();
  }, [user]); // Re-run effect if user data changes (e.g., after login)

  if (loading) {
    return (
      <>
        <Header />
        <div className="leases-container">
          Cargando tus alquileres...
        </div>
      </>
    );
  }

  if (error) {
    return (
      <>
        <Header />
        <div className="leases-container error-message">
          {error}
        </div>
      </>
    );
  }

  return (
    <>
      <Header />
      <div className="leases-container">
        <h1>Mis alquileres</h1>
        {leases.length === 0 ? (
          <p>No tienes alquileres en este momento.</p>
        ) : (
          <div className="leases-list">
            {leases.map((lease) => (
              <div key={lease.id} className="lease-card">
                <h3>{lease.publicationName}</h3>
                <p>
                  <strong>Fechas de Alquiler:</strong> {lease.fechaInicio} al {lease.fechaFin}
                </p>
                <p>
                  <strong>Precio Total :</strong> ${lease.totalPrice}
                </p>
                <p>
                  <strong>Fecha de Reserva:</strong> {new Date(lease.reservationDate).toLocaleDateString('es-AR', {
                    year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit'
                  })}
                </p>
                <p>
                  <strong>Tipo de Entrega:</strong> {lease.tipoEntrega}
                </p>
                {lease.tipoEntrega === 'A domicilio' && (
                  <p>
                    <strong>Dirección de Entrega:</strong> {lease.calleEntrega} {lease.numeroEntrega}
                  </p>
                )}
                {/* You can add more details from the lease object here if needed */}
              </div>
            ))}
          </div>
        )}
      </div>
    </>
  );
}

export default Leases;