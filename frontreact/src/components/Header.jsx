import { useAuth } from "../AuthContext";
import logo from '../assets/bobElAlquiladorLogoCompleto.svg'; 
import { useNavigate } from "react-router-dom";
import SideBarMenu from "./SideBarMenu";
import { Link } from 'react-router-dom';

function Header(){
    const navigate = useNavigate();
    const { user, logout } = useAuth();
    const handleLogout = () => {
    logout();
    navigate('/login');
  };
    return (
      
      <header className="home-header">
      <SideBarMenu/>
      <Link to="/HomePage">
        <img src={logo} alt="Logo Bob el Alquilador" style={{ width: '12em' }} />
      </Link>
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