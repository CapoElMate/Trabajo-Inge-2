// AuthContext.js
import { createContext, useContext, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true); // 👈 nuevo estado
  const navigate = useNavigate();

  useEffect(() => {
    const fetchUser = async () => {
      try {
        const response = await fetch(`http://localhost:5000/Auth/me`, {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          },
          credentials: "include",
        });

        if (!response.ok) {
          setUser(null);
        } else {
          const userData = await response.json();
          setUser(userData);
        }
      } catch (error) {
        console.error("Error fetching user:", error);
        setUser(null);
      } finally {
        setLoading(false); // 👈 termina la carga
      }
    };

    fetchUser();
  }, []);

 const login = async (email, password) => {
  try {
    // Verificar si el usuario existe
    const response = await fetch(`http://localhost:5000/api/Usuario/byEmail?email=${encodeURIComponent(email)}`);
    
    if (!response.ok) {
      if (response.status === 404) {
        setError("Mail o Contraseña incorrectas.");
        return;
      }
      throw new Error(`Error al buscar usuario. Estado: ${response.status}`);
    }

    const data = await response.json();

    if (!data || Object.keys(data).length === 0) {
      setError("Mail o Contraseña incorrectas.");
      return;
    }

    if (data.dniVerificado === false) {
      setError("Tu cuenta aún no fue verificada. Revisa tu correo.");
      return;
    }

    // Intentar iniciar sesión
    const loginResponse = await fetch(`http://localhost:5000/Auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ email, password, rememberMe: true }),
      credentials: "include",
    });

    if (loginResponse.status === 401) {
      setError("Mail o Contraseña incorrectas.");
      return;
    }

    if (!loginResponse.ok) {
      throw new Error(`Error durante el login. Estado: ${loginResponse.status}`);
    }

    await loginResponse.json(); // consumir cuerpo

    // Obtener datos del usuario logueado
    const responseMe = await fetch(`http://localhost:5000/Auth/me`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
    });

    if (!responseMe.ok) {
      throw new Error("Error al obtener el usuario luego del login.");
    }

    const usersFound = await responseMe.json();

    if (usersFound) {
      setUser(usersFound);
      setError(null);
      if (usersFound.roles.includes("Dueño")) {
        navigate("/HomePageAdmin");
      } else if (usersFound.roles.includes("Cliente")) {
        navigate("/HomePage");
      } else if (usersFound.roles.includes("Empleado")) {
        navigate("/EmployeeHome");
      }
    } else {
      setError("Error inesperado. Intenta nuevamente.");
    }
  } catch (error) {
    console.error("Error en login:", error);
    setError("Hubo un problema al iniciar sesión. Intenta más tarde.");
  }
};

  const logout = async () => {
    try {
      const response = await fetch(`http://localhost:5000/Auth/logout`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      await response.json(); // consumir cuerpo para evitar errores

      if (!response.ok) {
        throw new Error(
          `Error en la conexión o el servidor respondió con estado: ${response.status}`
        );
      }

      setUser(null);
      setError(null);
      navigate("/Login");
    } catch (error) {
      console.error("Error al cerrar sesión:", error);
    }
  };

  return (
    <AuthContext.Provider value={{ user, login, logout, error, loading }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
