import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './SignUp.css';

const SignUp = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        email: '',
        password: '',
        confirmPassword: '',
        dni: '',
        nombre: '',
        apellido: '',
        fecNacimiento: '',
        telefono: '',
        calle: '',
        altura: '',
        dpto: '',
        piso: '',
    });

    const [dniImageBase64, setDniImageBase64] = useState('');
    const [statusMessage, setStatusMessage] = useState('');
    const [error, setError] = useState(null);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({ ...prev, [name]: value }));
    };

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (!file) return;

        const validTypes = ['image/jpeg', 'image/png', 'image/jpg'];
        if (!validTypes.includes(file.type)) {
            setError('Formato de imagen inválido. Solo se permiten JPG, JPEG y PNG.');
            return;
        }

        const reader = new FileReader();
        reader.onloadend = () => {
            const base64String = reader.result.split(',')[1];
            setDniImageBase64(base64String);
        };

        reader.readAsDataURL(file);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError(null);
        setStatusMessage('Registrando usuario...');

        const authData = {
            email: formData.email,
            password: formData.password,
            confirmPassword: formData.confirmPassword,
            role: 'Empleado',
        };

        const usuarioRegistrado = {
            email: formData.email,
            dni: formData.dni,
            nombre: formData.nombre,
            apellido: formData.apellido,
            fecNacimiento: new Date(formData.fecNacimiento).toISOString(),
            telefono: formData.telefono,
            calle: formData.calle,
            altura: formData.altura,
            dpto: formData.dpto,
            piso: formData.piso,
            permisosEspeciales: [],
            roleName: 'Empleado',
            dniVerificado: true
        };

        try {
            const authResponse = await fetch('http://localhost:5000/Auth/register', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(authData),
            });

            if (!authResponse.ok) throw new Error(`Error en Auth: ${await authResponse.text()}`);

            const userResponse = await fetch('http://localhost:5000/api/Usuario', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(usuarioRegistrado),
            });

            if (!userResponse.ok) throw new Error(`Error en Usuario: ${await userResponse.text()}`);

            const clienteResponse = await fetch('http://localhost:5000/api/Cliente', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ usuarioRegistrado }),
            });

            if (!clienteResponse.ok) throw new Error(`Error en Cliente: ${await clienteResponse.text()}`);

            const empleadoResponse = await fetch('http://localhost:5000/api/Empleado', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({ cliente: { usuarioRegistrado } }),
            });

            if (!empleadoResponse.ok) throw new Error(`Error en Empleado: ${await empleadoResponse.text()}`);

            if (dniImageBase64) {
                const byteCharacters = atob(dniImageBase64);
                const byteArrays = [];

                for (let offset = 0; offset < byteCharacters.length; offset += 512) {
                    const slice = byteCharacters.slice(offset, offset + 512);
                    const byteNumbers = new Array(slice.length);
                    for (let i = 0; i < slice.length; i++) {
                        byteNumbers[i] = slice.charCodeAt(i);
                    }
                    const byteArray = new Uint8Array(byteNumbers);
                    byteArrays.push(byteArray);
                }

                const blob = new Blob(byteArrays, { type: 'image/jpeg' });

                const formDataArchivo = new FormData();
                formDataArchivo.append('EntidadID', parseInt(formData.dni));
                formDataArchivo.append('TipoEntidad', 3);
                formDataArchivo.append('Nombre', 'Archivo DNI');
                formDataArchivo.append('Descripcion', `Archivo DNI de ${formData.nombre} ${formData.apellido}`);
                formDataArchivo.append('Archivo', blob, 'dni.jpg');

                const dniResponse = await fetch('http://localhost:5000/api/Archivo', {
                    method: 'POST',
                    body: formDataArchivo,
                });

                if (!dniResponse.ok) {
                    throw new Error(`Error al enviar archivo DNI: ${await dniResponse.text()}`);
                }
            }
            const confirmDniResponse = await fetch(`http://localhost:5000/api/Cliente/ConfirmDNI?dni=${formData.dni}`, {
                    method: 'PUT',
                });

                if (!confirmDniResponse.ok) {
                    throw new Error(`Error al confirmar DNI: ${await confirmDniResponse.text()}`);
                }


            setStatusMessage('Usuario registrado correctamente');
            navigate("/Login");
        } catch (err) {
            console.error(err);
            setError(err.message);
            setStatusMessage('Ocurrió un error durante el registro.');
        }
    };

    return (
        <div className="signup-container">
            <h2>Formulario de Registro para nuevos empleados</h2>
            <form onSubmit={handleSubmit}>
                <input name="email" placeholder="Email" value={formData.email} onChange={handleChange} required />
                <input type="password" name="password" placeholder="Contraseña" value={formData.password} onChange={handleChange} required />
                <input type="password" name="confirmPassword" placeholder="Confirmar Contraseña" value={formData.confirmPassword} onChange={handleChange} required />
                <input name="dni" placeholder="DNI" value={formData.dni} onChange={handleChange} required />
                <input name="nombre" placeholder="Nombre" value={formData.nombre} onChange={handleChange} required />
                <input name="apellido" placeholder="Apellido" value={formData.apellido} onChange={handleChange} required />
                <input type="date" name="fecNacimiento" value={formData.fecNacimiento} onChange={handleChange} required />
                <input name="telefono" placeholder="Teléfono" value={formData.telefono} onChange={handleChange} required />
                <input name="calle" placeholder="Calle" value={formData.calle} onChange={handleChange} required />
                <input name="altura" placeholder="Altura" value={formData.altura} onChange={handleChange} required />
                <input name="dpto" placeholder="Dpto" value={formData.dpto} onChange={handleChange} />
                <input name="piso" placeholder="Piso" value={formData.piso} onChange={handleChange} />

                <label>Subir imagen del DNI (.jpg, .jpeg, .png):</label>
                <input type="file" accept="image/jpeg,image/jpg,image/png" onChange={handleFileChange} />

                <button type="submit">Registrarse</button>
            </form>

            {statusMessage && <p>{statusMessage}</p>}
            {error && <p className="error-message">Error: {error}</p>}
        </div>
    );
};

export default SignUp;
