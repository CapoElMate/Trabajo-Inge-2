import { createContext, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();

  const login = async (email, password) => {
    try {
      const response = await fetch(`http://localhost:3001/users?email=${email}&password=${password}`);

      if (!response.ok) {
        throw new Error(`Error en la conexión o el servidor respondió con estado: ${response.status}`);
      }

      const usersFound = await response.json();

      if (usersFound.length > 0) {
        const usuarioLogueado = usersFound[0];
        setUser(usuarioLogueado);
        setError(null); // limpiar error si login fue exitoso

        if (usuarioLogueado.rol === "admin") {
          navigate("/HomePageAdmin");
        } else if (usuarioLogueado.rol === "cliente") {
          navigate("/HomePage");
        }
      } else {
        setError("Email o contraseña incorrectos. Por favor, inténtalo de nuevo.");
      }
    } catch (error) {
      console.error("Error durante el login:", error);
      setError("Hubo un problema al intentar iniciar sesión. Por favor, inténtalo de nuevo más tarde.");
    }
  };

  const logout = () => {
    setUser(null);
    setError(null);
    navigate("/Login");
  };

  return (
    <AuthContext.Provider value={{ user, login, logout, error }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
