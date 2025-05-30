import { createContext, useContext, useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);
  const navigate = useNavigate();
  
  // 🟡 Cargar sesión desde LocalStorage
  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

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
        localStorage.setItem("user", JSON.stringify(usuarioLogueado)); // 🟢 Guardar en LocalStorage
        setError(null);

        if (usuarioLogueado.rol === "dueño") {
          navigate("/HomePageAdmin");
        } else if (usuarioLogueado.rol === "cliente") {
          navigate("/HomePage");
        }
        if(usuarioLogueado.rol==="empleado"){
          navigate("/EmployeeHome");
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
    localStorage.removeItem("user"); // 🔴 Limpiar sesión
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
