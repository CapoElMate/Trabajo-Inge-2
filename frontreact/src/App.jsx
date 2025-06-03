import { Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import HomePage from './components/HomePage';
import { AuthProvider } from './AuthContext';
import SignUp from './components/SignUp';
import ForgotPassword from './components/ForgotPassword';
import ValidateUser from './components/ValidateUser';
import PostDetail from './components/PostDetail';
import Header from './components/Header';

import CrearPublicacion from './components/Publicacion/CrearPublicacion';
import EditarPublicacion from './components/Publicacion/EditarPublicacion';
//import DuplicarPublicacion from  './components/Publicacion/DuplicarPublicacion';
import PublicacionDetail from  './components/Publicacion/DetallePublicacion';

import CrearMaquinaria from './components/Maquinaria/CrearMaquinaria';
import EditarMaquinaria from './components/Maquinaria/EditarMaquinaria';
//import DuplicarMaquinaria from './components/Maquinaria/DuplicarMaquinaria';
import ListarMaquinaria from './components/Maquinaria/ListarMaquinaria';

import DetalleReserva from './components/Reserva/DetalleReserva';
import DetalleAlquiler from './components/Alquiler/DetalleAlquiler';

import HomePageAdmin from './components/HomePageAdmin';
import './components/App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import Leases from './components/Leases';
import Rentals from './components/Rentals';
import ResetPassword from './components/ResetPassword';
import PermitFileUploader from './components/PermitFileUploader';
import Perfil from './components/Perfil/Perfil';
import ValidatePermit from './components/ValidatePermit';
import EmployeeHome from './components/EmployeeHome';
import ConfirmReservations from './components/ConfrmReservations';
const App = () => {
  return (
    <AuthProvider>
      <Routes>
        <Route path="/" element={<HomePage />} />
        <Route path="/Login" element={<Login />} />
        <Route path="/HomePage" element={<HomePage />} />
        <Route path="/ForgotPassword" element={<ForgotPassword />} />
        <Route path="/SignUp" element={<SignUp />} />
        <Route path="/ValidateUser" element={<ValidateUser />} />
        {/* <Route path="/PostDetail/:id" element={<PostDetail />} /> */}
        <Route path="/Header" element={<Header />} />
       
        <Route path="/CrearPublicacion" element={<CrearPublicacion />} />
        <Route path="/EditarPublicacion/:id" element={<EditarPublicacion />} />
        <Route path="/DetallePublicacion/:id" element={<PublicacionDetail />} />
       
        <Route path="/CrearMaquinaria" element={<CrearMaquinaria />} />
        <Route path="/ModificarMaquinaria/:id" element={<EditarMaquinaria />} />
        {/* <Route path="/DetalleMaquinaria/:id" element={<MaquinariaDetail />} /> */}
        <Route path="/ListarMaquinaria" element={<ListarMaquinaria />} />
        <Route path="/HomePageAdmin" element={<HomePageAdmin />} />

        <Route path="/DetalleReserva/:id" element={<DetalleReserva />} />
        <Route path="/DetalleAlquiler/:id" element={<DetalleAlquiler />} />

        <Route path="/Leases" element={<Leases />} />
        <Route path="/Rentals" element={<Rentals />} />
        <Route path="/Perfil" element={<Perfil />} />
        <Route path="/ResetPassword" element={<ResetPassword />} />
        <Route path="/PermitFileUploader" element={<PermitFileUploader />} />
        <Route path="/ValidatePermit" element={<ValidatePermit />} />
        <Route path="/EmployeeHome" element={<EmployeeHome />} />
        <Route path="/ConfirmReservations" element={<ConfirmReservations />} />
      </Routes>
    </AuthProvider>
  );
};

export default App;
