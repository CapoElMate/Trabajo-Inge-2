import React, { useState } from 'react';

function SignUp() {
  const [dniBase64, setDniBase64] = useState(null);

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    const reader = new FileReader();

    reader.onloadend = () => {
      const base64String = reader.result.split(',')[1];
      setDniBase64(base64String);
    };

    if (file) {
      reader.readAsDataURL(file);
    }
  };

  const handleSubmit = async () => {
    try {
      // Paso 1: Crear usuario de autenticación
      const authResponse = await fetch('http://localhost:5000/Auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'ejemplo@gmail.com',
          password: 'Password123',
          confirmPassword: 'Password123',
          role: 'Cliente'
        })
      });

      if (!authResponse.ok) throw new Error('Error creando autenticación');
      console.log("error de /register "+authResponse);
      // Paso 2: Crear usuario completo
      const userResponse = await fetch('http://localhost:5000/api/Usuario', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          email: 'ejemplo@correo.com',
          dni: '12345678',
          nombre: 'Juan',
          apellido: 'Pérez',
          edad: 30,
          telefono: '2214567890',
          calle: 'Falsa',
          altura: '123',
          dpto: '1',
          entreCalles: 'Primera y Segunda',
          permisosEspeciales: [],
          roleName: 'Cliente'
        })
      });

      if (!userResponse.ok) throw new Error('Error creando usuario');
      console.log(userResponse);
      const usuario = await userResponse.json();

      // Paso 3: Crear cliente
      const clienteResponse = await fetch('http://localhost:5000/api/Cliente', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(usuario)
      });

      if (!clienteResponse.ok) throw new Error('Error creando cliente');

      const cliente = await clienteResponse.json();

      // Paso 5: Subir archivo DNI si existe
      if (dniBase64) {
        const archivoResponse = await fetch('http://localhost:5000/api/Archivo', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            entidadID: cliente.dni, // Usa el DNI como entidadID
            tipoEntidad: 3,
            nombre: 'DNI',
            descripcion: `DNI de ${cliente.nombre}`,
            archivo: dniBase64
          })
        });

        if (!archivoResponse.ok) throw new Error('Error subiendo archivo DNI');
      }

      alert('Usuario registrado correctamente');
    } catch (error) {
      console.log(error);
      //console.error('Error:', error);
      alert('Ocurrió un error. Revisa la consola.');
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h2>Registro de Usuario</h2>
      <input type="file" accept="image/*" onChange={handleImageChange} />
      <br /><br />
      <button onClick={handleSubmit}>Registrar usuario con DNI</button>
    </div>
  );
}

export default SignUp;
