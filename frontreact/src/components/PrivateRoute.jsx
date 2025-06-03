import { useAuth } from "../AuthContext";
import { Navigate, Outlet, useLocation } from "react-router-dom";

const PrivateRoute = ({ rolesPermitidos }) => {
  const { user, loading } = useAuth();
  const location = useLocation();

  if (loading) {
    return <div>Cargando...</div>; //Si se borra esto el user siempre es null(no lo borren )
  }

  if (!user) {
    // Usuario no autenticado
    return <Navigate to="/Login" state={{ from: location }} replace />;
  }

  const tienePermiso = rolesPermitidos.some(rol => user.roles.includes(rol));

  if (!tienePermiso) {
    // Usuario autenticado pero sin permisos 
    return <Navigate to="/NoAutorizado" state={{ from: location }} replace />;
  }

  //SI es el user coin permisos y todo le da el componente que desea
  return <Outlet />;
};

export default PrivateRoute;