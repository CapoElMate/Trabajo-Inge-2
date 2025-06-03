import { useAuth } from "../AuthContext";
import logo from '../assets/bobElAlquiladorLogoCompleto.svg'; 
import { useNavigate } from "react-router-dom";
import SideBarMenu from "./SideBarMenu";
import { Link } from 'react-router-dom';
import './Header.css';

function Header(){
    const navigate = useNavigate();
    const { user, logout } = useAuth();
    
    
    const handleLogout = () => {
      logout();
      navigate('/login');
    }

    const logoRedirect=()=>{
      console.log("Goll"+user);
      if(user){
        if(user.roles.includes("Dueño")){
          navigate("/HomePageAdmin");
        }
        if(user.roles.includes("Empleado")){
            navigate("/EmployeeHome");  
        }
        if(user.roles.includes("Cliente")){
            navigate("/HomePage");  
        }
      }else{
        navigate("/HomePage");
      }

    };
    return (
      
      <header className="home-header">
      <SideBarMenu/>
        <img src={logo} alt="Logo Bob el Alquilador" onClick={logoRedirect} style={{ width: '12em' }} />

      {user ? (
        <button onClick={handleLogout} className="logout-button">
          Cerrar Sesión
        </button>
      ) : (
        <button onClick={()=>{navigate("/login")}} className="login-button">
          Iniciar Sesión
        </button>                                 
      )}
    </header>
    );
}
export default Header;