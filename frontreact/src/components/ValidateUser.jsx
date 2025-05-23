import React, { useState, useEffect } from 'react';
import './ValidateUser.css'; 

const ValidateUser = () => {
  const [pendingUsers, setPendingUsers] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchPendingUsers = async () => {
      try {
        const response = await fetch('http://localhost:3001/pendingUsers');

        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        setPendingUsers(data); 
      } catch (err) {
        setError(err.message); 
      } finally {
        setLoading(false); 
      }
    };

    fetchPendingUsers(); 
  }, []); 

  const handleApprove = async (user) => {
    
    try {
      const userToApprove = { ...user };
      delete userToApprove.id; 
      delete userToApprove.confirmPassword;
        const addResponse = await fetch('http://localhost:3001/users', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(userToApprove),
      });

      if (!addResponse.ok) {
        throw new Error(`Failed to add user to validated users: ${addResponse.status}`);
      }
      const addedUser = await addResponse.json(); 

      const deleteResponse = await fetch(`http://localhost:3001/pendingUsers/${user.id}`, {
        method: 'DELETE',
      });

      if (!deleteResponse.ok) {
        console.warn(`Usuario ${user.id} aprobado pero falló la eliminación de pendingUsers.`);
        throw new Error(`Failed to delete user from pendingUsers: ${deleteResponse.status}`);
      }
      const emailRegistrationResponse = await fetch('http://localhost:3001/passwordResetRequests', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          email: user.email, 
          approvedAt: new Date().toISOString(), 
          type: 'user_approved_email_registration' 
        }),
      });

      if (!emailRegistrationResponse.ok) {
        console.warn(`Usuario ${user.email} aprobado, pero falló el registro del email en passwordResetRequests.`);
      }

      setPendingUsers(pendingUsers.filter((u) => u.id !== user.id));
      alert(`Usuario ${user.nombre} ${user.apellido} aprobado, se envió el mail de confirmación.`);
      console.log('Usuario movido:', addedUser);
      console.log('Email registrado en passwordResetRequests:', user.email);

    } catch (err) {
      console.error('Error al aprobar usuario:', err);
      alert(`Error al aprobar usuario: ${err.message}`);
    }
  };

  const handleReject = async (id) => {
    try {
      const response = await fetch(`http://localhost:3001/pendingUsers/${id}`, {
        method: 'DELETE',
      });

      if (!response.ok) {
        throw new Error(`Failed to delete pending user: ${response.status}`);
      }

      setPendingUsers(pendingUsers.filter((user) => user.id !== id));
      alert('Usuario rechazado y eliminado de la lista de pendientes.');
    } catch (err) {
      console.error('Error al rechazar usuario:', err);
      alert(`Error al rechazar usuario: ${err.message}`);
    }
  };

  if (loading) {
    return <div className="validate-user-container">Cargando usuarios pendientes...</div>;
  }

  if (error) {
    return <div className="validate-user-container error-message">Error: {error}</div>;
  }

  return (
    <div className="validate-user-container">
      <h2>Usuarios Pendientes de Validación</h2>
      {pendingUsers.length === 0 ? (
        <p>No hay usuarios pendientes de validación en este momento.</p>
      ) : (
        <div className="users-grid">
          {pendingUsers.map((user) => (
            <div key={user.id} className="user-card">
              <h3>{user.nombre} {user.apellido}</h3>
              <p><strong>DNI:</strong> {user.dni}</p>
              <p><strong>Edad:</strong> {user.edad}</p>
              <p><strong>Email:</strong> {user.email}</p>
              <p><strong>Teléfono:</strong> {user.telefono}</p>
              <p><strong>Dirección:</strong> {user.calle} {user.altura} {user.departamento && `Dpto: ${user.departamento}`}</p>
              {user.fotoDNI_name && (
                <p><strong>Foto DNI:</strong> <a href={`#`} onClick={(e) => { e.preventDefault(); alert(`Simulando descarga de ${user.fotoDNI_name}`); }}>{user.fotoDNI_name}</a></p>
              )}
              <div className="card-actions">
                <button
                  className="approve-btn"
                  onClick={() => handleApprove(user)}
                >
                  Aprobar
                </button>
                <button
                  className="reject-btn"
                  onClick={() => handleReject(user.id)}
                >
                  Rechazar
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default ValidateUser;