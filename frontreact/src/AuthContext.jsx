import { createContext, useContext, useState } from "react";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const navigate = useNavigate();

  const login = async (email, password) => { 
    try {
      const response = await fetch(`http://localhost:3000/users?email=${email}&password=${password}`);

      if (!response.ok) {
        throw new Error(`Error en la conexión o el servidor respondió con estado: ${response.status}`);
      }

      const usersFound = await response.json(); 

      if (usersFound.length > 0) {
        const usuarioLogueado = usersFound[0]; 
        setUser(usuarioLogueado); 
        navigate("/homePage"); 
      } else {
        alert("Email o contraseña incorrectos");
      }
    } catch (error) {
      console.error("Error durante el login:", error);
      alert("Hubo un problema al intentar iniciar sesión. Por favor, inténtalo de nuevo más tarde.");
    }
  };

  const logout = () => {
    setUser(null); 
    navigate("/Login"); 
  };

  return (
    <AuthContext.Provider value={{ user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);