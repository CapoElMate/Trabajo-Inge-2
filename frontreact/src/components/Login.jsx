import logo from '../assets/bobElAlquiladorLogoCompleto.svg';
import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import './Login.css';
function Login() {
  const navigate = useNavigate();
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  
  const handleLogin = (e) => {
    e.preventDefault();
    if (username === 'david' && password === '1234') {
      navigate('/HomePage');
    } else {
      alert('Credenciales incorrectas');
    }
  };

  return (
    <div className="login-container">
      <form onSubmit={handleLogin} className="login-form">
        <img src={logo} alt="Bob el alquilador logo" style={{width:'300px'}} />
        <h2>Iniciar sesión</h2>

        <input
          type="text"
          placeholder="email@example.com"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <input
          type="password"
          placeholder="Contraseña"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit">Entrar</button>

        <div className="login-links">
          <Link to="/ForgotPassword">¿Olvidaste tu contraseña?</Link>
          <Link to="/SignUp">Registrarse</Link>
        </div>
      </form>
    </div>
  );
}
export default Login;
