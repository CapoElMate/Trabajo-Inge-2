import { useAuth } from "../AuthContext";
import logo from '../assets/bobElAlquiladorLogoCompleto.svg'; 
import { useNavigate } from "react-router-dom";
function Header(){
    const navigate = useNavigate();
    const { user, logout } = useAuth();
    const handleLogout = () => {
    logout();
    navigate('/login');
  };
    return (
        
      <header className="home-header">
      <h1>Bienvenido {user ? user.nombre : ''}</h1>
      <img src={logo} alt="Logo Bob el Alquilador" style={{width:'300px'}}/>
      {user ? (
        <button onClick={handleLogout} className="logout-button">
          Cerrar Sesión
        </button>
      ) : (
        <button onClick={() => navigate('/login')} className="login-button">
          Iniciar Sesión
        </button>
      )}
    </header>
    );
}
export default Header;