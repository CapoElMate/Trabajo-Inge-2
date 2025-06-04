import React, { useState } from 'react';
import './ForgotPassword.css'; 

function ForgotPassword() {
  const [email, setEmail] = useState('');
  const [message, setMessage] = useState('');
  const [isError, setIsError] = useState(false);
  const [isLoading, setIsLoading] = useState(false);

  const validarInput = (input) => {
    return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(input) || /^\d{10}$/.test(input);
  };

  const handleSearch = async (e) => { 
    e.preventDefault();

    setMessage(''); 
    setIsError(false);
    setIsLoading(true); 

    if (!validarInput(email)) {
      setMessage('Por favor, ingresa un correo electrónico válido.');
      setIsError(true);
      setIsLoading(false);
      return;
    }

    try {

      const response = await fetch('http://localhost:5000/Auth/forgot-password', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: email, 
        }),
      });

      if (response.ok) {
        const result = await response.json();
        setMessage('En caso de que exista un mail asociado se enviaran los pasos para la restauracion de contraseña');
        setIsError(false);
        console.log('Solicitud de recuperación registrada en JSON Server:', result);
      } else {
        setMessage('Ocurrió un error al procesar tu solicitud. Por favor, inténtalo de nuevo.');
        setIsError(true);
        console.error('Error al registrar solicitud', response.status);
      }
    } catch (error) {
      console.error('Error al enviar solicitud:', error);
      setMessage('Hubo un problema de conexión. Por favor, verifica que JSON Server esté corriendo.');
      setIsError(true);
    } finally {
      setIsLoading(false);
    }
  };

  const handleCancel = () => {
    setEmail('');
    setMessage('');
    setIsError(false);
    setIsLoading(false);
  };

  return (
    <div className="forgot-password-container">
      <div className="forgot-password-card">
        <h2>Recuperar Contraseña</h2>
        <p className="instruction-text">
          Ingresa tu correo electrónico.
        </p>
        <form onSubmit={handleSearch}>
          <input
            type="text"
            placeholder="Correo electrónico "
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            className={isError ? 'input-error' : ''}
            disabled={isLoading}
          />
          {message && (
            <p className={`message ${isError ? 'error' : 'success'}`}>
              {message}
            </p>
          )}
          <div className="button-group">
            <button
              type="button"
              className="cancel-button"
              onClick={handleCancel}
              disabled={isLoading} 
            >
              Cancelar
            </button>
            <button
              type="submit"
              className="search-button"
              disabled={isLoading}
            >
              {isLoading ? 'Buscando...' : 'Restablecer contraseña'}
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default ForgotPassword;