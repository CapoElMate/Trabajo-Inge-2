import { useState, useEffect } from "react";
import { useAuth } from "../AuthContext"; 
import { Link } from "react-router-dom"; 
import { useNavigate } from "react-router-dom";

import './Login.css';

const Login = () => {
  const { login, error } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [errorMessage, setErrorMessage] = useState(""); 
  const navigate = useNavigate();  

  const handleSubmit = async (e) => { 
    e.preventDefault();
    setErrorMessage(""); 

    if (!email || !password) {
      setErrorMessage("Por favor, ingresa tu email y contraseña.");
      return;
    }

    try {
      await login(email, password);
    } catch (error) {
      console.error("Error en el componente Login:", error);
      setErrorMessage("Hubo un problema al iniciar sesión. Inténtalo de nuevo.");
    }
  };

  // Limpiar errores al tipear nuevamente
  useEffect(() => {
    if (errorMessage || error) {
      setErrorMessage("");
    }
  }, [email, password]);

  return (
    <div className="login-container"> 
      <div className="login-card"> 
        <h2>Iniciar Sesión</h2>
        <form onSubmit={handleSubmit}>
          <input
            type="email"
            placeholder="Correo electrónico"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required 
          />
          <input
            type="password"
            placeholder="Contraseña"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          {(errorMessage || error) && (
            <p className="error-message">{errorMessage || error}</p>
          )}

          <button type="submit">Ingresar</button>
        </form>

        <Link to="/ForgotPassword" className="forgot-password-link">¿Olvidaste tu contraseña?</Link>
        <Link to="/SignUp" className="register-link">¿No tienes cuenta? Regístrate</Link>
      </div>
    </div>
  );
};

export default Login;
