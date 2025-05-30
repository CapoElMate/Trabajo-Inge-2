import React, { useState } from 'react';

function SignUp() {
  const [dniBase64, setDniBase64] = useState(null);

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    const reader = new FileReader();

    reader.onloadend = () => {
      setDniBase64(reader.result);
    };

    if (file) {
      reader.readAsDataURL(file);
    }
  };

  // Función auxiliar para convertir Base64 a Blob
  function base64ToBlob(base64, contentType) {
    // Elimina el prefijo "data:image/png;base64," para obtener solo los caracteres Base64
    const byteCharacters = atob(base64.split(',')[1]);
    const byteNumbers = new Array(byteCharacters.length);
    for (let i = 0; i < byteCharacters.length; i++) {
      byteNumbers[i] = byteCharacters.charCodeAt(i);
    }
    const byteArray = new Uint8Array(byteNumbers);
    return new Blob([byteArray], { type: contentType });
  }

  const handleSubmit = async () => {
    try {

      var jsonRegister = JSON.stringify({
          email: 'ejemplo3@gmail.com',
          password: 'Password123!',
          confirmPassword: 'Password123!',
          role: 'Cliente'
        });

        var jsonUsuario = JSON.stringify({
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

        var jsonCliente = JSON.stringify({usuarioRegistrado: JSON.parse(jsonUsuario)});



        //console.log("jsonRegister: "+jsonRegister);
        //console.log("jsonUsuario: "+jsonUsuario);
        //console.log("cliente: "+ jsonCliente);

      //Paso 1: Crear usuario de autenticación
      const authResponse = await fetch('http://localhost:5000/Auth/register', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: jsonRegister,
      });

      const errorData = await authResponse.json(); // Intenta parsear el cuerpo como JSON

      console.error('Error de registro (400 Bad Request):', authResponse.status);
      for (const key in errorData) {
          if (Object.hasOwnProperty.call(errorData, key)) {
              const errors = errorData[key];
              if (Array.isArray(errors)) {
                  errors.forEach(errorMessage => {
                      if (key === "") {
                          console.error(`Error general: ${errorMessage}`);
                      } else {
                          console.error(`Error en el campo '${key}': ${errorMessage}`);
                      }
                  });
              }
          }
      }
      if (!authResponse.ok) throw new Error('Error creando autenticación');

      //Paso 2: Crear usuario completo
      const userResponse = await fetch('http://localhost:5000/api/Usuario', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: jsonUsuario,
      });

      if (!userResponse.ok) throw new Error('Error creando usuario');
      console.log(userResponse);

      // Paso 3: Crear cliente
      const clienteResponse = await fetch('http://localhost:5000/api/Cliente', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: jsonCliente,
      });

      
      const cliente = await clienteResponse.json();
      console.log(clienteResponse);
      
      if (!clienteResponse.ok) throw new Error('Error creando cliente');
      
      //Paso 4: Subir archivo DNI si existe
      if (dniBase64) {
        let archivoBlob;
        let fileName = 'dni_document.png';

        const parts = dniBase64.match(/^data:(.*?);base64,(.*)$/);
        if (!parts) {
            console.error("Formato de Base64 del DNI inválido.");
            alert('Error: Formato de imagen DNI inválido.');
            return;
        }
        const contentType = parts[1];

        // **Asegurarse de que el tipo de contenido sea permitido antes de crear el Blob**
        const allowedImageTypes = ['image/jpeg', 'image/png'];
        if (!allowedImageTypes.includes(contentType)) {
            console.error(`Tipo de archivo no permitido: ${contentType}`);
            alert('Error: Solo se permiten imágenes JPG y PNG.');
            return; // Detiene la ejecución si el tipo no es válido
        }

        archivoBlob = base64ToBlob(dniBase64, contentType);

        // Inferir el nombre y extensión del archivo basado en el contentType
        if (contentType === 'image/jpeg') {
            fileName = 'dni_image.jpeg';
        } else if (contentType === 'image/png') {
            fileName = 'dni_image.png';
        } else {
             // Esto no debería ejecutarse si la validación anterior funciona, pero es una buena práctica
            fileName = 'dni_image_unknown.jpg';
        }

        const formData = new FormData();
        formData.append('EntidadID', cliente.dni);
        formData.append('TipoEntidad', 3);
        formData.append('Nombre', 'DNI');
        formData.append('Descripcion', `DNI de ${cliente.nombre} ${cliente.apellido}`);
        formData.append('Archivo', archivoBlob, fileName);

        const archivoResponse = await fetch('http://localhost:5000/api/Archivo', {
          method: 'POST',
          body: formData,
        });

        if (!archivoResponse.ok) {
          const errorBody = await archivoResponse.json();
          console.error('Error subiendo archivo DNI:', archivoResponse.status, errorBody);
          throw new Error('Error subiendo archivo DNI.');
        }
        console.log('Archivo DNI subido exitosamente.');
      }

      alert('Usuario registrado correctamente!');
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
