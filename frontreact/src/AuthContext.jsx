  import { createContext, useContext, useState } from "react";
  import { useNavigate } from "react-router-dom";

  const AuthContext = createContext();

  export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const navigate = useNavigate();

    const login = async (email, password) => {
      try {
        const response = await fetch("http://localhost:5000/api/Auth/login", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          }, credentials:'include',// Permite enviar y recibir cookies
          body: JSON.stringify({ 
            email, 
            password,
            rememberMe: true}),
        });
        console.log(response);
        if (!response.ok) {
          throw new Error(`Error del servidor: ${response.status}`);
        }

        const user = await response.json();
        console.log(user);
        if (user) {
          setUser(user); 
          if (user.rol === "admin") {
            navigate("/HomePageAdmin");
          } else if (user.rol === "cliente") {
            navigate("/HomePage");
          }
        } else {
          alert("Email o contraseña incorrectos");
        }
      } catch (error) {
        console.error("Error durante el login:", error);
        alert("Hubo un problema al iniciar sesión. Intenta más tarde.");
      }

      try
      {
        const response2 = await fetch("http://localhost:5000/api/Auth/me", {
          method: "GET",
          headers: {
            "Content-Type": "application/json",
          }, credentials:'include',
        });
        console.log(response2);
        const responseBody = await response2.json();
        console.log(responseBody);
        if (!response2.ok) {
          throw new Error(`Error del servidor: ${response2.status}`);
        }
      }
      catch (error) {
        console.error("Error durante el login:", error);
        alert("Hubo un problema al iniciar sesión. Intenta más tarde.");
      }
      
      try
      {
        const response3 = await fetch("http://localhost:5000/api/Auth/logout", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          }, credentials:'include',
        });
        console.log(response3);
        const responseBody2 = await response3.json();
        console.log(responseBody2);
        if (!response3.ok) {
          throw new Error(`Error del servidor: ${response3.status}`);
        }
      }
      catch (error) {
        console.error("Error durante el login:", error);
        alert("Hubo un problema al iniciar sesión. Intenta más tarde.");
      }      
    };

    const logout = () => {
      setUser(null);
      navigate("/login"); // Ajusta la ruta según tu app
    };

    return (
      <AuthContext.Provider value={{ user, login, logout }}>
        {children}
      </AuthContext.Provider>
    );
  };

  export const useAuth = () => useContext(AuthContext);
