import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

function SignUp() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post('/api/register', {
        email,
        password
      });

      console.log('Registrado', response.data);
      setSuccess('Registro exitoso');
      setError('');

      // Redirigir al usuario después de 1 segundo (opcional)
      setTimeout(() => {
        navigate('/LoginReal'); // o a donde quieras enviarlo
      }, 1000);

    } catch (err) {
      setError(err.response?.data?.message || 'Error en el registro');
      setSuccess('');
    }
  };

  return (
    <div>
      <h2>SignUp</h2>
      <form onSubmit={handleSubmit}>
        <input
          type="email"
          placeholder="Correo"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        /><br />

        <input
          type="password"
          placeholder="Contraseña"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        /><br />

        <button type="submit">SignUp</button>
      </form>

      {error && <p style={{ color: 'red' }}>{error}</p>}
      {success && <p style={{ color: 'green' }}>{success}</p>}
    </div>
  );
}

export default SignUp;
