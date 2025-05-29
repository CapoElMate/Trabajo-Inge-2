import { useNavigate } from "react-router-dom";
import Header from "./Header";
function HomePageAdmin(){
   const navigate = useNavigate();
    return (<>
    <Header/>
    <div>
        <span>Registrar un Empleado</span>
        <button onClick={()=>navigate("/SignUp")}>ir</button>       
    </div>
    </>);
}
export default HomePageAdmin;