import { useNavigate } from "react-router-dom";
import Header from "./Header";
function Profile(){
    const navigate = useNavigate();
    return (<>
    <Header/>
    {/*Endpoint a el usuario*/}
    <h1>Perfil de usuario</h1>
    <button onClick={()=>{navigate("/ResetPassword");}}>Cambiar Contraseña</button>
    
    <button onClick={()=>{navigate("/PermitFileUploader");}}>Cargar Permiso de Maquinarias</button>
    <button onClick={()=>{navigate("/ResetPassword")}}>Cambiar constraseña</button>
    </>);
}
export default Profile;