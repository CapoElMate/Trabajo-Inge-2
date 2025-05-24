import './HomePage.css';
import logo from '../assets/bobElAlquiladorLogoCompleto.svg';
export default function Header2(){

    return(
         <header className="homepage-header">
                <img src={logo} alt="Bob el alquilador" style={{width:'200px'}} />
         </header>
    )
}