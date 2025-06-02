import { Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import HomePage from './components/HomePage';
import { AuthProvider } from './AuthContext';
import SignUp from './components/SignUp';
import ForgotPassword from './components/ForgotPassword';
import ValidateUser from './components/ValidateUser';
import PostDetail from './components/PostDetail';
import Header from './components/Header';

import CargarPublicacion from './components/CargarPublicacion';
import EditarPublicacion from './components/EditarPublicacion';
import DuplicarPublicacion from  './components/DuplicarPublicacion';
import PublicacionDetail from  './components/DetallePublicacion';

import CargarMaquinaria from './components/CargarMaquinaria';
import EditarMaquinaria from './components/EditarMaquinaria';
import DuplicarMaquinaria from './components/DuplicarMaquinaria';
import MaquinariaDetail from './components/DetalleMaquinaria';
import MaquinariaList from './components/MaquinariaList';


import HomePageAdmin from './components/HomePageAdmin';
import './components/App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import Leases from './components/Leases';
import Rentals from './components/Rentals';
import ResetPassword from './components/ResetPassword';
import PermitFileUploader from './components/PermitFileUploader';
import Profile from './components/Profile';
import ValidatePermit from './components/ValidatePermit';
import EmployeeHome from './components/EmployeeHome';
import ConfirmReservations from './components/ConfrmReservations';
import PublicacionEjemplo from './components/PublicacionEjemplo';
const App = () => {
  return (
    <AuthProvider>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/HomePage" element={<HomePage />} />
        <Route path="/ForgotPassword" element={<ForgotPassword />} />
        <Route path="/SignUp" element={<SignUp />} />
        <Route path='/ValidateUser' element={<ValidateUser />}/> 
        <Route path='/PostDetail/:id' element={<PostDetail />}/> 
        <Route path='/Header' element={<Header/>} />
        <Route path='/CargarPublicacion' element={<CargarPublicacion/>} />
        <Route path='/EditarPublicacion/:id' element={<EditarPublicacion/>} />
        <Route path="/DuplicarPublicacion/:id" element={<DuplicarPublicacion/>}/>
        <Route path="/DetallePublicacion/:id" element={<PublicacionDetail/>}/>
        <Route path='/CargarMaquinaria' element={<CargarMaquinaria/>} />
        <Route path="/modificar/:id" element={<EditarMaquinaria />} />
        <Route path="/duplicar/:id" element={<DuplicarMaquinaria />} />       
        <Route path="/maquinaria/:id" element={<MaquinariaDetail />} />
        <Route path='/maquinarias' element={<MaquinariaList/>}/>
        <Route path='/HomePageAdmin' element={<HomePageAdmin/>} />
        <Route path='/Leases' element={<Leases/>}/>
        <Route path='/Rentals' element={<Rentals/>}/>      
        <Route path='/Profile' element={<Profile/>}/>
        <Route path='/ResetPassword' element={<ResetPassword/>}/>
        <Route path='/PermitFileUploader' element={<PermitFileUploader/>}/>
        <Route path='/ValidatePermit' element={<ValidatePermit/>}/>
        <Route path='/EmployeeHome' element={<EmployeeHome/>}/>
        <Route path='/ConfirmReservations' element={<ConfirmReservations/>}/>
        <Route path='/PublicacionEjemplo' element={<PublicacionEjemplo/>}/>
      </Routes>
    </AuthProvider>
  );
};

export default App;
