  import { createContext, useContext, useState } from "react";
  import { useNavigate } from "react-router-dom";

  const AuthContext = createContext();

  export const AuthProvider = ({ children }) => {
    const [user, setUser] = useState(null);
    const navigate = useNavigate();
    const twoFactorCode = "string";
   const twoFactorRecoveryCode= "string";

    const login = async (email, password) => {
      try {
        const response = await fetch("http://localhost:5000/login", {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          }, credentials:'include',// Permite enviar y recibir cookies
          body: JSON.stringify({ email, password,twoFactorCode,twoFactorRecoveryCode}),
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
