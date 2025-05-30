import React, { useState, useEffect } from 'react';
import Header from "./Header";
import './ConfirmReservations.css'; // Make sure this CSS file exists

function ConfirmReservations() {
  const [pendingRentals, setPendingRentals] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [message, setMessage] = useState({ type: '', text: '' }); // For success/error messages after actions

  useEffect(() => {
    const fetchPendingRentals = async () => {
      try {
        const response = await fetch('http://localhost:3001/pendingRentals');
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setPendingRentals(data);
      } catch (err) {
        console.error("Error fetching pending rentals:", err);
        setError("No se pudieron cargar las reservas pendientes. Intenta de nuevo más tarde.");
      } finally {
        setLoading(false);
      }
    };

    fetchPendingRentals();
  }, []);

  // --- Handle Accept Rental ---
  const handleAcceptRental = async (rental) => {
    setMessage({ type: '', text: '' }); // Clear previous messages
    try {
      // 1. Add the rental to /leases
      const leaseResponse = await fetch('http://localhost:3001/leases', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ ...rental, status: 'accepted' }), // Add a status
      });

      if (!leaseResponse.ok) {
        throw new Error(`HTTP error! status: ${leaseResponse.status} adding to leases.`);
      }

      // 2. Delete the rental from /pendingRentals
      const deleteResponse = await fetch(`http://localhost:3001/pendingRentals/${rental.id}`, {
        method: 'DELETE',
      });

      if (!deleteResponse.ok) {
        throw new Error(`HTTP error! status: ${deleteResponse.status} deleting from pending rentals.`);
      }

      // Update local state by removing the accepted rental
      setPendingRentals(prevRentals => prevRentals.filter(r => r.id !== rental.id));
      setMessage({ type: 'success', text: `Reserva de "${rental.publicationName}" aceptada y movida a Arriendos.` });

    } catch (err) {
      console.error("Error accepting rental:", err);
      setMessage({ type: 'error', text: `Error al aceptar la reserva: ${err.message}` });
    }
  };

  // --- Handle Reject Rental ---
  const handleRejectRental = async (rentalId, publicationName) => {
    setMessage({ type: '', text: '' }); // Clear previous messages
    try {
      // 1. Delete the rental from /pendingRentals
      const deleteResponse = await fetch(`http://localhost:3001/pendingRentals/${rentalId}`, {
        method: 'DELETE',
      });

      if (!deleteResponse.ok) {
        throw new Error(`HTTP error! status: ${deleteResponse.status} deleting from pending rentals.`);
      }

      // Update local state by removing the rejected rental
      setPendingRentals(prevRentals => prevRentals.filter(r => r.id !== rentalId));
      setMessage({ type: 'success', text: `Reserva de "${publicationName}" rechazada y eliminada.` });

    } catch (err) {
      console.error("Error rejecting rental:", err);
      setMessage({ type: 'error', text: `Error al rechazar la reserva: ${err.message}` });
    }
  };

  if (loading) {
    return (
      <>
        <Header />
        <div className="confirm-reservations-container">
          Cargando reservas pendientes...
        </div>
      </>
    );
  }

  if (error) {
    return (
      <>
        <Header />
        <div className="confirm-reservations-container error-message">
          {error}
        </div>
      </>
    );
  }

  return (
    <>
      <Header />
      <div className="confirm-reservations-container">
        <h1>Reservas en Estado de Validación Pendiente</h1>
        {message.text && (
          <div className={`action-message ${message.type}`}>
            {message.text}
          </div>
        )}
        {pendingRentals.length === 0 ? (
          <p>No hay reservas pendientes en este momento.</p>
        ) : (
          <div className="rentals-list">
            {pendingRentals.map((rental) => (
              <div key={rental.id} className="rental-card">
                <h3>Reserva de {rental.publicationName}</h3>
                <p>
                  <strong>Cliente:</strong> {rental.clientName} {rental.clientApellido}
                </p>
                <p>
                  <strong>DNI Cliente:</strong> {rental.clientDni}
                </p>
                <p>
                  <strong>Maquinaria a Alquilar:</strong> {rental.publicationName}
                </p>
                <p>
                  <strong>Precio por día:</strong> ${rental.publicationPricePerDay.toLocaleString('es-AR')}
                </p>
                <p>
                  <strong>Precio Total Estimado:</strong> ${rental.totalPrice}
                </p>
                <p>
                  <strong>Fecha de Reserva:</strong> {new Date(rental.reservationDate).toLocaleDateString('es-AR', {
                    year: 'numeric', month: 'long', day: 'numeric', hour: '2-digit', minute: '2-digit'
                  })}
                </p>
                <p>
                  <strong>Período de Alquiler:</strong> {rental.fechaInicio} al {rental.fechaFin}
                </p>
                <p>
                  <strong>Tipo de Entrega:</strong> {rental.tipoEntrega}
                </p>
                {rental.tipoEntrega === 'A domicilio' && (
                  <p>
                    <strong>Dirección de Entrega:</strong> {rental.calleEntrega} {rental.numeroEntrega}
                  </p>
                )}
                <div className="rental-actions">
                  <button
                    className="accept-button"
                    onClick={() => handleAcceptRental(rental)}
                  >
                    Aceptar
                  </button>
                  <button
                    className="reject-button"
                    onClick={() => handleRejectRental(rental.id, rental.publicationName)}
                  >
                    Rechazar
                  </button>
                </div>
              </div>
            ))}
          </div>
        )}
      </div>
    </>
  );
}

export default ConfirmReservations;