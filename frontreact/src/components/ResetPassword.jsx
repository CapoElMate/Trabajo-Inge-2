import React, { useState } from 'react';
import { useSearchParams } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';
function ResetPassword() {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const token = searchParams.get('token');
  const email = searchParams.get('email');

  const [newPassword, setNewPassword] = useState('');
  const [confirmNewPassword, setConfirmNewPassword] = useState('');
  const [message, setMessage] = useState('');
  const [isError, setIsError] = useState(false);
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage('');
    setIsError(false);

    if (newPassword !== confirmNewPassword) {
      setMessage('Contraseña de confirmación incorrecta, inténtalo nuevamente');// cambiar como en la HU
      setIsError(true);
      return;
    }

    if (!newPassword || newPassword.length < 8) {
      setMessage('La contraseña debe tener un minimo de 8 caracteres');
      setIsError(true);
      return;
    }

    setLoading(true);

    try {
      const response = await fetch('http://localhost:5000/Auth/reset-password', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email,
          token,
          newPassword,
          confirmNewPassword
        })
      });

      const result = await response.json();

      if (response.ok) {
        setMessage('Cambio de contraseña exitoso');
        navigate("/Login")
      } else {
        setMessage(result.message || 'Hubo un error al cambiar la contraseña.');
        setIsError(true);
      }
    } catch (error) {
      setMessage('Error de conexión con el servidor.');
      setIsError(true);
    } finally {
      setLoading(false);
    }
  };

  if (!token || !email) {
    return <p>URL inválida. Faltan parámetros.</p>;
  }

  return (
    <div className="reset-password-container">
      <h2>Cambiar Contraseña</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="password"
          placeholder="Nueva contraseña"
          value={newPassword}
          onChange={(e) => setNewPassword(e.target.value)}
          disabled={loading}
        />
        <input
          type="password"
          placeholder="Confirmar nueva contraseña"
          value={confirmNewPassword}
          onChange={(e) => setConfirmNewPassword(e.target.value)}
          disabled={loading}
        />
        {message && (
          <p style={{ color: isError ? 'red' : 'green' }}>{message}</p>
        )}
        <button type="submit" disabled={loading}>
          {loading ? 'Cambiando...' : 'Cambiar contraseña'}
        </button>
      </form>
    </div>
  );
}

export default ResetPassword;
