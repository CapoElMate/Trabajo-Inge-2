import React, { useState } from 'react'; // Import useState for hover effect
import { useNavigate } from "react-router-dom";
import { useAuth } from "../AuthContext"; // Corrected import for useAuth
import Header from "./Header";
import './Profile.css'; // We'll create this CSS file

function Profile() {
  const { user } = useAuth(); // Destructure user from useAuth()
  const navigate = useNavigate();

  // State for hover effects
  const [isResetPasswordHovered, setIsResetPasswordHovered] = useState(false);
  const [isPermitUploaderHovered, setIsPermitUploaderHovered] = useState(false);

  return (
    <>
      <Header />
      <div className="profile-container">
        <h1>Perfil de Usuario</h1>

        {/* Display user information if available */}
        {user ? (
          <div className="user-info-card">
            <p><strong>Nombre:</strong> {user.nombre}</p>
            <p><strong>Apellido:</strong> {user.apellido}</p>
            <p><strong>DNI:</strong> {user.dni}</p>
            <p><strong>Email:</strong> {user.email}</p>
            {/* Add more user details as needed */}
          </div>
        ) : (
          <p>Cargando información del usuario...</p>
        )}

        <div className="profile-actions">
          {/* Change Password Card */}
          <div
            className="action-card"
            onClick={() => navigate("/ResetPassword")}
            onMouseEnter={() => setIsResetPasswordHovered(true)}
            onMouseLeave={() => setIsResetPasswordHovered(false)}
          >
            {isResetPasswordHovered ? (
              <span className="card-text">Ir a Cambiar Contraseña</span>
            ) : (
              <span className="card-text">Cambiar Contraseña</span>
            )}
          </div>

          {/* Conditional rendering for Permit Uploader Card */}
          {user && user.rol !== "dueño" && ( // Only show if user exists and rol is NOT "dueño"
            <div
              className="action-card"
              onClick={() => navigate("/PermitFileUploader")}
              onMouseEnter={() => setIsPermitUploaderHovered(true)}
              onMouseLeave={() => setIsPermitUploaderHovered(false)}
            >
              {isPermitUploaderHovered ? (
                <span className="card-text">Ir a Cargar Permiso de Maquinarias</span>
              ) : (
                <span className="card-text">Cargar Permiso de Maquinarias</span>
              )}
            </div>
          )}
        </div>
      </div>
    </>
  );
}

export default Profile;