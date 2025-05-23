import { useAuth } from "../AuthContext";

const HomePage = () => {
  const { user, logout } = useAuth();

  return (
    <div>
      {user ? (
        <>
          <h2>Bienvenido, {user.nombre}</h2>
          <p>Tu rol es: {user.rol}</p>
          <button onClick={logout}>Cerrar sesión</button>
        </>
      ) : (
        <p>No hay sesión activa</p>
      )}
    </div>
  );
};

export default HomePage;
