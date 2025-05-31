import React, { useState } from 'react';

import './SignUp.css';
import { data, useNavigate } from "react-router-dom";
import { useAuth } from '../AuthContext';
import Header from './Header';
const SignUp = () => {
  const [mensajeExito, setMensajeExito] = useState('');
  const {user} = useAuth();
  const navigate = useNavigate();
  const [formData, setFormData] = useState({
    nombre: '',
    apellido: '',
    dni: '',
    edad: '',
    telefono: '',
    calle: '',
    altura: '',
    departamento: '',
    entreCalles: '',
    email: '',
    password: '',
    confirmPassword: '',
    fotoDNI: null,
    rol:'Cliente',
  });

  const [errors, setErrors] = useState({});
  const convertirArchivoABase64 = (file) => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.onload = () => resolve(reader.result); // Devuelve el string base64 completo (data:<tipo>;base64,...)
    reader.onerror = reject;
    reader.readAsDataURL(file);
  });
};
  const validarEmail = (email) =>
    /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);

  const validarPassword = (pass) =>
    /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$/.test(pass);

  const validarArchivo = (file) => {
    if (!file) return false;
    const ext = file.name.toLowerCase().split('.').pop();
    return ['jpg', 'jpeg', 'png', 'pdf'].includes(ext);
  };

  const handleChange = (e) => {
    const { name, value, files } = e.target;
    setFormData({
      ...formData,
      [name]: files ? files[0] : value,
    });
  };

  const validate = () => {
    const newErrors = {};

    if (formData.nombre.length < 2 || formData.nombre.length > 50)
      newErrors.nombre = 'Nombre debe tener entre 2 y 50 caracteres';

    if (formData.apellido.length < 2 || formData.apellido.length > 50)
      newErrors.apellido = 'Apellido debe tener entre 2 y 50 caracteres';

    const edad = parseInt(formData.edad);
    if (isNaN(edad) || edad < 18 || edad > 120)
      newErrors.edad = 'Edad debe ser entre 18 y 120';

    if (!/^\d{3}\s?\d{3,4}\s?\d{3,4}$/.test(formData.telefono))
      newErrors.telefono = 'Formato: 221 555 5555 o similar';

    if (!formData.calle || !formData.altura)
      newErrors.direccion = 'Debe ingresar Calle y Altura';

    if (!/^\d{7,8}$/.test(formData.dni))
      newErrors.dni = 'DNI debe tener 7 u 8 dígitos';

    if (!validarEmail(formData.email) || formData.email.length > 254)
      newErrors.email = 'Email inválido o demasiado largo';

    if (!formData.fotoDNI || !validarArchivo(formData.fotoDNI))
      newErrors.fotoDNI = 'Debe adjuntar un archivo .jpg, .jpeg, .png o .pdf';

    if (formData.password !== formData.confirmPassword)
      newErrors.confirmPassword = 'Las contraseñas no coinciden';

    if (!validarPassword(formData.password))
      newErrors.password =
        'Mínimo 6 caracteres. Debe incluir letras, números y un símbolo especial';

    setErrors(newErrors);
    return Object.keys(newErrors).length === 0;
  };
    const handleSubmit = async (e) => {
  e.preventDefault();
  //let ruta = 'pendingUsers';

  if (!validate()) return;

  try {
    // 1. Traer todos los usuarios actuales
    const res = await fetch('http://localhost:5000/api/Usuario/all');
    const usuarios = await res.json();

    // 2. Verificar si ya existe un usuario con ese email o DNI
    const emailExistente = usuarios.find(u => u.email === formData.email);
    const dniExistente = usuarios.find(u => u.dni === formData.dni);

    if (emailExistente || dniExistente) {
      const newErrors = {};
      if (emailExistente) newErrors.email = 'Ya existe un usuario con este email.';
      if (dniExistente) newErrors.dni = 'Ya existe un usuario con este DNI.';
      setErrors(newErrors);
      return;
    }

    const dataToSend = { ...formData };
    delete dataToSend.confirmPassword;

    // 3. Convertir imagen a base64 si está presente
    if (formData.fotoDNI) {
      const base64 = await convertirArchivoABase64(formData.fotoDNI);
      dataToSend.fotoDNI = base64;
      dataToSend.fotoDNI_name = formData.fotoDNI.name;
    }

    // 4. Si es dueño quien registra, cambia a 'empleado'
    if (user && user.rol === 'dueño') {
      dataToSend.rol = 'Empleado';
      //ruta = 'users';
    }

    // 5. Enviar al backend
    //1-Registrar Auth
    const responseAuth = await fetch(`http://localhost:5000/Auth/register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        "email": dataToSend.email,
        "password": dataToSend.password,
        "confirmPassword": dataToSend.password,
        "role": dataToSend.rol,
      }),
    });

    console.log('Auth registrada:', await responseAuth.json());

    if (responseAuth.ok) {
      //2-Registrar Usuario
      const responseRegistrarUsuario = await fetch(`http://localhost:5000/api/Usuario`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          "email": dataToSend.email,
          "password": dataToSend.password,
          "dni": dataToSend.dni,
          "nombre": dataToSend.nombre,
          "apellido": dataToSend.apellido,
          "edad": dataToSend.edad,
          "telefono": dataToSend.telefono,
          "calle": dataToSend.calle,
          "altura": dataToSend.altura,
          "dpto": dataToSend.departamento,
          "entreCalles": dataToSend.entreCalles,
          "permisosEspeciales": [],
          "roleName": dataToSend.rol,
          "dniVerificado": false,
        }),
      });

      console.log('Usuario registrado:', await responseRegistrarUsuario.json());
      
      if (responseRegistrarUsuario.ok) {
        //3-Registrar Cliente
        const responseRegistrarCliente = await fetch(`http://localhost:5000/api/Cliente`, {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify({
              "usuarioRegistrado": {
              "email": dataToSend.email,
              "password": dataToSend.password,
              "dni": dataToSend.dni,
              "nombre": dataToSend.nombre,
              "apellido": dataToSend.apellido,
              "edad": dataToSend.edad,
              "telefono": dataToSend.telefono,
              "calle": dataToSend.calle,
              "altura": dataToSend.altura,
              "dpto": dataToSend.departamento,
              "entreCalles": dataToSend.entreCalles,
              "permisosEspeciales": [],
              "roleName": dataToSend.rol,
              "dniVerificado": false,
            }
          }),
        });

        console.log('Cliente registrado:', await responseRegistrarCliente.json());

        if (!responseRegistrarCliente.ok) {
          if(dataToSend.rol === 'Empleado') {
            //3-Registrar Cliente
            const responseRegistrarEmpleado = await fetch(`http://localhost:5000/api/Empleado`, {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json',
              },
              body: JSON.stringify({
                "cliente": {
                    "usuarioRegistrado": {
                    "email": dataToSend.email,
                    "password": dataToSend.password,
                    "dni": dataToSend.dni,
                    "nombre": dataToSend.nombre,
                    "apellido": dataToSend.apellido,
                    "edad": dataToSend.edad,
                    "telefono": dataToSend.telefono,
                    "calle": dataToSend.calle,
                    "altura": dataToSend.altura,
                    "dpto": dataToSend.departamento,
                    "entreCalles": dataToSend.entreCalles,
                    "permisosEspeciales": [],
                    "roleName": dataToSend.rol,
                    "dniVerificado": false,
                  }
                }
              }),
            });

            console.log('Empleado registrado:', await responseRegistrarEmpleado.json());
          }

          if (!user) {
            setMensajeExito('Sus datos serán verificados en breve por un empleado de Bob el Alquilador');
            setTimeout(() => {
              navigate("/HomePage");
            }, 3000); // Espera 3 segundos antes de redirigir
          } else if (user.rol === 'dueño') {
            setMensajeExito('Se ha registrado un empleado exitosamente');
          }
        }
      }
    }


  } catch (error) {
    console.error('Error al enviar el formulario:', error);
    alert('Hubo un problema de conexión. Por favor, inténtalo de nuevo.');
  }
};

  


  return (<>
    {mensajeExito && (
  <div className="popup-exito">
    <p>{mensajeExito}</p>
  </div>
)}
    {user&&(<Header/>)}
    <div className="signup-container">
      <form onSubmit={handleSubmit}>
        <h2>Registro</h2>

        <input type="text" name="nombre" placeholder="Nombre" onChange={handleChange} value={formData.nombre} />
        {errors.nombre && <p className="error-message">{errors.nombre}</p>}

        <input type="text" name="apellido" placeholder="Apellido" onChange={handleChange} value={formData.apellido} />
        {errors.apellido && <p className="error-message">{errors.apellido}</p>}

        <input type="text" name="dni" placeholder="DNI" onChange={handleChange} value={formData.dni} />
        {errors.dni && <p className="error-message">{errors.dni}</p>}

        <label htmlFor="fotoDNI" className="file-label">Foto DNI (frente):</label>
        <input type="file" name="fotoDNI" id="fotoDNI" accept=".jpg,.jpeg,.png,.pdf" onChange={handleChange} />
        {errors.fotoDNI && <p className="error-message">{errors.fotoDNI}</p>}

        <input type="number" name="edad" placeholder="Edad" onChange={handleChange} value={formData.edad} />
        {errors.edad && <p className="error-message">{errors.edad}</p>}

        <input
          type="text"
          name="telefono"
          placeholder="Código y número (Ej: 221 555 5555)"
          onChange={handleChange}
          value={formData.telefono}
        />
        {errors.telefono && <p className="error-message">{errors.telefono}</p>}

        <input type="text" name="calle" placeholder="Calle" onChange={handleChange} value={formData.calle} />
        <input type="text" name="altura" placeholder="Altura" onChange={handleChange} value={formData.altura} />
        <input type="text" name="departamento" placeholder="Departamento (opcional)" onChange={handleChange} value={formData.departamento} />
        <input type="text" name="entreCalles" placeholder="Entre calles (opcional)" onChange={handleChange} value={formData.entreCalles} />
        {errors.direccion && <p className="error-message">{errors.direccion}</p>}

        <input type="email" name="email" placeholder="Email" onChange={handleChange} value={formData.email} />
        {errors.email && <p className="error-message">{errors.email}</p>}

        <input type="password" name="password" placeholder="Contraseña" onChange={handleChange} value={formData.password} />
        {errors.password && <p className="error-message">{errors.password}</p>}

        <input type="password" name="confirmPassword" placeholder="Confirmar Contraseña" onChange={handleChange} value={formData.confirmPassword} />
        {errors.confirmPassword && <p className="error-message">{errors.confirmPassword}</p>}

        <button type="submit">Registrarse</button>
      </form>
    </div>
    </>
  );
};

export default SignUp;