import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import './PostDetail.css'; // Create this CSS file
import { useAuth } from '../AuthContext';
import Header from './Header';
import { useRef } from 'react';
function PostDetail() {

  const section=useRef(null);
  const { id } = useParams(); // Get the publication ID from the URL
  const navigate = useNavigate();
  const [publicacion, setPublicacion] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [showReservationForm, setShowReservationForm] = useState(false);
  const {user} = useAuth();
  // Reservation form state
  const [fechaInicio, setFechaInicio] = useState('');
  const [fechaFin, setFechaFin] = useState('');
  const [tipoEntrega, setTipoEntrega] = useState(''); // 'A domicilio', 'Retiro en sucursal'
  const [calleEntrega, setCalleEntrega] = useState('');
  const [numeroEntrega, setNumeroEntrega] = useState('');
  const [formErrors, setFormErrors] = useState({});
  const [reservationMessage, setReservationMessage] = useState({ type: '', text: '' });
  useEffect(() => {
    const fetchPublicacion = async () => {
      try {
        const response = await fetch(`http://localhost:3001/publicaciones/${id}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        setPublicacion(data);
      } catch (error) {
        console.error("Error fetching publication details:", error);
        setError("No se pudo cargar la publicación. Intenta de nuevo más tarde.");
      } finally {
        setLoading(false);
      }
    };

    fetchPublicacion();
  }, [id]);

  const validateReservationForm = () => {
    let errors = {};
    const today = new Date();
    today.setHours(0, 0, 0, 0); // Normalize to start of day for comparison

    if (!fechaInicio) {
      errors.fechaInicio = 'La fecha de inicio es obligatoria.';
    } else if (new Date(fechaInicio) < today) {
      errors.fechaInicio = 'La fecha de inicio no puede ser en el pasado.';
    }
    if (!fechaFin) {
      errors.fechaFin = 'La fecha de fin es obligatoria.';
    } else if (new Date(fechaFin) < new Date(fechaInicio)) {
      errors.fechaFin = 'La fecha de fin no puede ser anterior a la fecha de inicio.';
    }

    if (!tipoEntrega) {
      errors.tipoEntrega = 'El tipo de entrega es obligatorio.';
    } else if (tipoEntrega === 'A domicilio') {
      if (!calleEntrega.trim()) {
        errors.calleEntrega = 'La calle de entrega es obligatoria para entrega a domicilio.';
      }
      if (!numeroEntrega.trim()) {
        errors.numeroEntrega = 'El número de entrega es obligatorio para entrega a domicilio.';
      } else if (isNaN(numeroEntrega) || parseInt(numeroEntrega) <= 0) {
        errors.numeroEntrega = 'El número de entrega debe ser un número válido.';
      }
    }

    setFormErrors(errors);
    return Object.keys(errors).length === 0;
  };

  const calculateTotalPrice = () => {
    if (!fechaInicio || !fechaFin || !publicacion) return 'N/A';

    const startDate = new Date(fechaInicio);
    const endDate = new Date(fechaFin);
    const timeDiff = endDate.getTime() - startDate.getTime();
    const daysDiff = Math.ceil(timeDiff / (1000 * 3600 * 24)) + 1; // +1 to include both start and end day

    if (daysDiff <= 0) return 'N/A';

    return (daysDiff * publicacion.precioPorDia).toLocaleString('es-AR');
  };

  const handleReservationSubmit = async (e) => {
    e.preventDefault();
    
    setFormErrors({});
    setReservationMessage({ type: '', text: '' });

    if (!validateReservationForm()) {
      setReservationMessage({ type: 'error', text: 'Por favor, corrige los errores en el formulario.' });
      return;
    }

    // Simulate payment success (as per requirement)
    const paymentSuccessful = true; // This would typically involve a payment gateway

    if (paymentSuccessful) {
      try {
        // Update publication availability in db.json
        const updatedPublicacion = { ...publicacion, disponible: false };
        const response = await fetch(`http://localhost:3001/publicaciones/${id}`, {
          method: 'PUT', // Use PUT to replace the resource
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(updatedPublicacion),
        });

        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        setPublicacion(updatedPublicacion); // Update local state
        setReservationMessage({ type: 'success', text: `La reserva de la maquinaria "${publicacion.nombreMaquina}" se registró con éxito. La publicación ahora no está disponible.` });
        setShowReservationForm(false); // Hide the form after success
      } catch (err) {
        console.error("Error updating publication availability:", err);
        setReservationMessage({ type: 'error', text: 'Hubo un error al registrar la reserva y actualizar la disponibilidad. Intenta de nuevo.' });
      }
    } else {
      setReservationMessage({ type: 'error', text: 'El pago no pudo ser procesado. Intenta de nuevo.' });
    }
  };

  if (loading) {
    return <div className="detalle-container">Cargando detalles de la publicación...</div>;
  }

  if (error) {
    return <div className="detalle-container error-message">{error}</div>;
  }

  if (!publicacion) {
    return <div className="detalle-container">Publicación no encontrada.</div>;
  }

  return (
    <>
    <Header/>
    <div className="detalle-container">
      <button onClick={() => navigate('/HomePage')} className="back-button">Volver al inicio</button>
      <h1>Detalle de la Publicación</h1>
      <div className="publicacion-detalle-card">
        <div className="detalle-imagen">
          <img src={publicacion.imagenes[0] || 'https://via.placeholder.com/600x400/CCCCCC/000000?text=No+Image'} alt={publicacion.nombreMaquina} />
        </div>
        <div className="detalle-info">
          <h2>{publicacion.nombreMaquina}</h2>
          <p>
            <strong>Precio por día:</strong> ${publicacion.precioPorDia.toLocaleString('es-AR')}
          </p>
          <p>
            <strong>Ubicación:</strong> {publicacion.ubicacionActual.calle},{' '}
            {publicacion.ubicacionActual.altura}
            {publicacion.ubicacionActual.departamento && `, Dpto. ${publicacion.ubicacionActual.departamento}`}
            {publicacion.ubicacionActual.entreCalles && ` (${publicacion.ubicacionActual.entreCalles})`}
          </p>
          <p>
            <strong>Política de Cancelación:</strong> {publicacion.politicaCancelacion}
          </p>
          {publicacion.tagsAdicionales && publicacion.tagsAdicionales.length > 0 && (
            <p className="detalle-tags">
              <strong>Tags:</strong> {publicacion.tagsAdicionales.join(', ')}
            </p>
          )}
          <p className={`disponibilidad-status ${publicacion.disponible ? 'disponible' : 'no-disponible'}`}>
            Estado: {publicacion.disponible ? 'Disponible' : 'No Disponible'}
          </p>

          {publicacion.disponible ? (
            <button
              className="reservar-button"
              onClick={() => {
                if(!user){
                  navigate("/Login");
                  return;
                }
                setShowReservationForm(!showReservationForm)
                section.current?.scrollIntoView({behavior:"smoth"})}}
            >
              {showReservationForm ? 'Cerrar Formulario de Reserva' : 'Reservar Maquinaria'}
            </button>
          ) : (
            <button className="reservar-button disabled" disabled>
              No Disponible para Reserva
            </button>
          )}
        </div>
      </div>

      {showReservationForm && publicacion.disponible && (
        <div ref={section} className="reservation-form-card">
          <h3>Formulario de Reserva</h3>
          {reservationMessage.text && (
            <div className={`message ${reservationMessage.type}`}>
              {reservationMessage.text}
            </div>
          )}
          <form onSubmit={handleReservationSubmit}>
            <div className="form-group">
              <label htmlFor="fechaInicio">Fecha Inicio de Alquiler:</label>
              <input
                type="date"
                id="fechaInicio"
                value={fechaInicio}
                onChange={(e) => setFechaInicio(e.target.value)}
                min={new Date().toISOString().split('T')[0]} // Cannot select past dates
                required
              />
              {formErrors.fechaInicio && <span className="error-text">{formErrors.fechaInicio}</span>}
            </div>
            <div className="form-group">
              <label htmlFor="fechaFin">Fecha Fin de Alquiler:</label>
              <input
                type="date"
                id="fechaFin"
                value={fechaFin}
                onChange={(e) => setFechaFin(e.target.value)}
                min={fechaInicio || new Date().toISOString().split('T')[0]} // Cannot select past dates or before start date
                required
              />
              {formErrors.fechaFin && <span className="error-text">{formErrors.fechaFin}</span>}
            </div>

            <div className="form-group">
              <label htmlFor="tipoEntrega">Tipo de Entrega:</label>
              <select
                id="tipoEntrega"
                value={tipoEntrega}
                onChange={(e) => setTipoEntrega(e.target.value)}
                required
              >
                <option value="">Seleccione</option>
                <option value="A domicilio">A domicilio</option>
                <option value="Retiro en sucursal">Retiro en sucursal</option>
              </select>
              {formErrors.tipoEntrega && <span className="error-text">{formErrors.tipoEntrega}</span>}
            </div>

            {tipoEntrega === 'A domicilio' && (
              <>
                <div className="form-group">
                  <label htmlFor="calleEntrega">Calle de Entrega:</label>
                  <input
                    type="text"
                    id="calleEntrega"
                    value={calleEntrega}
                    onChange={(e) => setCalleEntrega(e.target.value)}
                    required
                  />
                  {formErrors.calleEntrega && <span className="error-text">{formErrors.calleEntrega}</span>}
                </div>
                <div className="form-group">
                  <label htmlFor="numeroEntrega">Número de Entrega:</label>
                  <input
                    type="text"
                    id="numeroEntrega"
                    value={numeroEntrega}
                    onChange={(e) => setNumeroEntrega(e.target.value)}
                    required
                  />
                  {formErrors.numeroEntrega && <span className="error-text">{formErrors.numeroEntrega}</span>}
                </div>
              </>
            )}

            <div className="form-group">
              <label>Monto Total Estimado:</label>
              <p className="total-amount">${calculateTotalPrice()}</p>
            </div>

            <button type="submit" className="submit-reservation-button">
              Confirmar Reserva
            </button>
          </form>
        </div>
      )}
    </div>
  </>
  );
}

export default PostDetail;