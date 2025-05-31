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
      const response = await fetch(`http://localhost:5000/Auth/login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
          "email": email,
          "password": password,
          "rememberMe": true
        }),
        credentials: 'include'
      });

      if (!response.ok) {
        throw new Error(`Error en la conexión o el servidor respondió con estado: ${response.status}`);
      }

      console.log("Respuesta del servidor:", await response.json());

      const responseMe = await fetch(`http://localhost:5000/Auth/me`, {
      method: 'GET',
      headers: {
        'Content-Type': 'application/json',
      },
      credentials: 'include'
      });

      let usersFound = await responseMe.json();



      if (usersFound) {
        setUser(usersFound);
        //localStorage.setItem("user", JSON.stringify(usuarioLogueado)); // 🟢 Guardar en LocalStorage
        setError(null);

        if (usersFound.roles.includes("Dueño")) {
          navigate("/HomePageAdmin");
        } else if (usersFound.roles.includes("Cliente")) {
          navigate("/HomePage");
        }
        if(usersFound.roles.includes("Empelado")){
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

  const logout = async () => {
    //localStorage.removeItem("user"); // 🔴 Limpiar sesión
    const response = await fetch(`http://localhost:5000/Auth/logout`, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    credentials: 'include'
    });

    console.log("Respuesta del servidor al cerrar sesión:", await response.json());
    if (!response.ok) {
      throw new Error(`Error en la conexión o el servidor respondió con estado: ${response.status}`);
    }
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
