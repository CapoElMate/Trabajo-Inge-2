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
      const response = await fetch(`http://localhost:5000/api/Usuario/byEmail?email=${encodeURIComponent(email)}`);
      if (!response.ok) {
        throw new Error(
          `Error en la conexión o el servidor respondió con estado: ${response.status}`
        );
      }const data=await response.json();
      console.log(data);
      if(data.dniVerificado == false){
        alert("No esta verificado");
      }else{
        try {
      const response = await fetch(`http://localhost:5000/Auth/login`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password, rememberMe: true }),
        credentials: "include",
      });

      if (!response.ok) {
        throw new Error(
          `Error en la conexión o el servidor respondió con estado: ${response.status}`
        );
      }

      await response.json(); // consumir para evitar errores de cuerpo ya leído

      const responseMe = await fetch(`http://localhost:5000/Auth/me`, {
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
      });

      const usersFound = await responseMe.json();

      if (usersFound) {
        setUser(usersFound);
        setError(null);
        console.log(usersFound);
        if (usersFound.roles.includes("Dueño")) {
          navigate("/HomePageAdmin");
        } else if (usersFound.roles.includes("Cliente")) {
          navigate("/HomePage");
        } else if (usersFound.roles.includes("Empleado")) {
          navigate("/EmployeeHome");
        }
      } else {
        setError("Email o contraseña incorrectos. Por favor, inténtalo de nuevo.");
      }
    } catch (error) {
      console.error("Error durante el login:", error);
      setError("Hubo un problema al intentar iniciar sesión. Por favor, inténtalo de nuevo más tarde.");
    }

      }
    } catch (error) {
      console.error("Error al cerrar sesión:", error);
      return null;
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
