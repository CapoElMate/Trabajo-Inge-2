import React, { useEffect, useState } from 'react';

const SignUp = () => {
    const [statusMessage, setStatusMessage] = useState('Enviando datos...');
    const [error, setError] = useState(null);

    useEffect(() => {
        const registerUser = async () => {

            const authData = {
                email: "user1Testuwu@gmail.com",
                password: "Password@1",
                confirmPassword: "Password@1",
                role: "Cliente"
            };

            // api/Usuario
            const userData = {
                email: "user1Testuwu@gmail.com",
                dni: "11111111",
                nombre: "mate",
                apellido: "etma",
                fecNacimiento: new Date().toISOString(),
                telefono: "22156732",
                calle: "1",
                altura: "1",
                dpto: "1",
                piso: "1",
                permisosEspeciales: [],
                roleName: "Cliente",
                dniVerificado: true
            };

            try {
                setStatusMessage('Registrando usuario en Auth...');
                const authResponse = await fetch('http://localhost:5000/Auth/register', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(authData)
                });

                if (!authResponse.ok) {
                    const errorText = await authResponse.text();
                    throw new Error(`Error en el registro de Auth: ${authResponse.status} - ${errorText}`);
                }

                const authResult = await authResponse.json();
                console.log('Auth registration successful:', authResult);
                setStatusMessage('Usuario registrado en Auth. Ahora registrando datos de usuario...');


                const userResponse = await fetch('http://localhost:5000/api/Usuario', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(userData)
                });

                if (!userResponse.ok) {
                    const errorText = await userResponse.text();
                    throw new Error(`Error en el registro de Usuario: ${userResponse.status} - ${errorText}`);
                }

                const userResult = await userResponse.json();
                console.log('User data registration successful:', userResult);
                setStatusMessage('Usuario y datos registrados exitosamente!');

            } catch (err) {
                console.error('Error during registration process:', err);
                setError(err.message);
                setStatusMessage(`Error: ${err.message}`);
            }
        };

        registerUser();
    }, []);

    return (
        <div style={{ padding: '20px', textAlign: 'center' }}>
            <h1>Proceso de Registro Automático</h1>
            <p>{statusMessage}</p>
            {error && <p style={{ color: 'red' }}>Detalle del Error: {error}</p>}
            <p>
                Este componente envía datos de registro fijos al backend
                sin interacción del usuario. Por favor, revisa la consola del navegador
                para ver los resultados detallados.
            </p>
        </div>
    );
};

export default SignUp;