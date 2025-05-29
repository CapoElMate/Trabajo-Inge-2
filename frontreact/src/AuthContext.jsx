import { createContext, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useEffect } from "react";
const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const login = async (email, password) => {
    setError(null); // Limpiar errores anteriores

    try {
      // 1. Login
      const loginResponse = await fetch("http://localhost:5000/api/Auth/login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include", // Enviar cookies
        body: JSON.stringify({ email, password, rememberMe: true }),
      });

      if (!loginResponse.ok) {
        throw new Error(`Login fallido: ${loginResponse.status}`);
      }

      // 2. Obtener info del usuario (si login fue exitoso)
      const meResponse = await fetch("http://localhost:5000/api/Auth/me", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      if (!meResponse.ok) {
        throw new Error(`Error al obtener usuario: ${meResponse.status}`);
      }

      const userData = await meResponse.json();
      console.log("Usuario autenticado:", userData);
      setUser(userData); // Guardar en contexto

      const rol = userData.roles?.[0]; // Tomar el primer rol

    if (rol === "Dueño") {
      navigate("/HomePageAdmin");
    } else if (rol === "Cliente") {
      navigate("/HomePage");
    } else {
      setError("Rol desconocido");
    }

    } catch (error) {
      console.error("Error durante el login:", error);
      setError("No se pudo iniciar sesión. Revisa tus credenciales o intenta más tarde.");
    }
  };

  const logout = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Auth/logout", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      if (!response.ok) {
        throw new Error(`Logout fallido: ${response.status}`);
      }

      setUser(null);
      setError(null);
      navigate("/Login");
    } catch (error) {
      console.error("Error durante logout:", error);
      setError("No se pudo cerrar sesión correctamente.");
    }
  };
  useEffect(() => {
  const checkAuth = async () => {
    try {
      const response = await fetch("http://localhost:5000/api/Auth/me", {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      if (!response.ok) {
        throw new Error("Usuario no autenticado");
      }

      const userData = await response.json();
      console.log("Sesión existente detectada:", userData);
      setUser(userData);

      // Redirigir según rol si estás en /login u otra ruta no autorizada
      const rol = userData.roles?.[0];
      if (rol === "Dueño") {
        navigate("/HomePageAdmin");
      } else if (rol === "Cliente") {
        navigate("/HomePage");
      }
    } catch (error) {
      console.log("No hay sesión activa:", error.message);
      // No hacer nada si no está autenticado
    }
  };

  checkAuth();
}, []);

  return (
    <AuthContext.Provider value={{ user, login, logout, error }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
