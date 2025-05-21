// Login.jsx

import React from 'react';
import './Login.css';
import loginVideo from '../assets/Bob_el_Alquilador_-_New_York.mp4';

const Login = () => {
  return (
    <div className="login-page">
      <div className="login-main">
        <form className="login-form">
          <h2>Iniciar sesión</h2>
          
          <label htmlFor="email" className="sr-only">Correo electrónico</label>
          <input type="email" id="email" placeholder="Correo electrónico" required />

          <label htmlFor="password" className="sr-only">Contraseña</label>
          <input type="password" id="password" placeholder="Contraseña" required />

          <button type="submit">Entrar</button>
        </form>

        <video className="login-video" src={loginVideo} autoPlay loop muted />
      </div>
    </div>
  );
};

export default Login;
