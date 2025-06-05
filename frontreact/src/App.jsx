import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import PrivateRoute from "./components/PrivateRoute";
import { AuthProvider } from "./AuthContext";

// Páginas
import Login from "./components/Login";
import HomePage from "./components/HomePage";
import HomePageAdmin from "./components/HomePageAdmin";
import EmployeeHome from "./components/EmployeeHome";
import NoAutorizado from "./pages/NoAutorizado";
import DetallePublicacion from "./components/publicacion/DetallePublicacion";
import "./components/App.css";
import "bootstrap/dist/css/bootstrap.min.css";
import SignUp from "./components/SignUp";
import SignUpEmpleado from "./components/SignUpEmpledo";
import ForgotPassword from "./components/ForgotPassword";
import Rentals from "./components/Rentals";

import CrearPublicacion from "./components/Publicacion/CrearPublicacion";
import ModificarPublicacion from "./components/Publicacion/ModificarPublicacion";

import CrearMaquinaria from "./components/Maquinaria/CrearMaquinaria";
import EditarMaquinaria from "./components/Maquinaria/EditarMaquinaria";
import ListarMaquinaria from "./components/Maquinaria/ListarMaquinaria";

import DetalleReserva from "./components/Reserva/DetalleReserva";
import DetalleAlquiler from "./components/Alquiler/DetalleAlquiler";
import FiltrarAlquiler from "./components/Alquiler/FiltrarAlquileres";
import ReservaFilter from "./components/Reserva/FiltrarReservas";

import Perfil from "./components/Perfil/Perfil";
import ResetPassword from "./components/ResetPassword";
import SolicitudesDeRegistro from "./components/SolicitudesDeRegistro";
import DetalleUsuario from "./components/DetalleUsuario";
import PermitFileUploeader from "./components/PermitFileUploader";
import PermisosDeUsuarios from "./components/PermisosDeUsuarios";

function App() {
  return (
    <AuthProvider>
      <Routes>
        
        {/*Crear un empleado, la dejo aqui para no tener que loguearse*/}
        <Route path="/SignUpEmpleado" element={<SignUpEmpleado />} />
        {/*Vistas publicas-------------------------------------------------------------*/}
        
        <Route path="/DetalleUsuario/:dni" element={<DetalleUsuario />} />
        
        <Route path="/resetPassword" element={<ResetPassword/>}/>
        <Route path="/login" element={<Login />} />
        <Route path="/noautorizado" element={<NoAutorizado />} />
        <Route path="/HomePage" element={<HomePage />} />
        <Route path="/" element={<HomePage />} />
        <Route path="/DetallePublicacion/:id" element={<DetallePublicacion />}/>
        <Route path="/SignUp" element={<SignUp />} />
        <Route path="/ForgotPassword" element={<ForgotPassword />} />

        {/* Vistas Cliente -------------------------------------------------------------*/}
        <Route element={<PrivateRoute rolesPermitidos={["Cliente"]} />}>
            <Route path="DetallePublicacion/:id/success" element={<DetallePublicacion />} />
            <Route path="DetallePublicacion/:id/failure" element={<DetallePublicacion />} />
            <Route path="DetallePublicacion/:id/pending" element={<DetallePublicacion />} />
          <Route path="/Perfil" element={<Perfil />} />
          <Route path="/PermitFileUploeader" element={<PermitFileUploeader/>}/>
        </Route>

        {/* Vistas Dueño -------------------------------------------------------------*/}
        <Route element={<PrivateRoute rolesPermitidos={["Dueño"]} />}>
          <Route path="/HomePageAdmin" element={<HomePageAdmin />} />
          <Route path="/CrearPublicacion" element={<CrearPublicacion />} />
          <Route path="/ModificarPublicacion/:id" element={<ModificarPublicacion />}/>
          <Route path="/HomePage" element={<HomePage />} />
          <Route path="/CrearMaquinaria" element={<CrearMaquinaria />} />
          <Route path="/ModificarMaquinaria/:id" element={<EditarMaquinaria />}/>
          <Route path="/ListarMaquinaria" element={<ListarMaquinaria />} />
          <Route path="/DetalleUsuario/:dni" element={<DetalleUsuario />} />
          <Route path="/SignUpEmpleado" element={<SignUpEmpleado />} />
          <Route path="/FiltrarAlquileres" element={<FiltrarAlquiler />} />
          <Route path="/FiltrarReservas" element={<ReservaFilter />} />
        </Route>


        {/* Empleado-------------------------------------------------------------*/}
        <Route element={<PrivateRoute rolesPermitidos={["Empleado"]} />}>
          <Route path="/EmployeeHome" element={<EmployeeHome />} />
          <Route path="/DetalleReserva/:id" element={<DetalleReserva />} />
          <Route path="/DetalleAlquiler/:id" element={<DetalleAlquiler />} />
          <Route path="/FiltrarAlquileres" element={<FiltrarAlquiler />} />
          <Route path="/FiltrarReservas" element={<ReservaFilter />} />
          <Route path="/PermisosDeUsuarios" element={<PermisosDeUsuarios />} />
          <Route path="/solicitudesDeRegistro" element={<SolicitudesDeRegistro/>}/>
        </Route>
        {/*
          Una forma de tener una vista para distintos roles
          <Route element={<PrivateRoute rolesPermitidos={["Dueño", "Empleado"]} />}>
                <Route path="/panel" element={<PanelComun />} />
              </Route>
            */}
      </Routes>
    </AuthProvider>
  );
}

export default App;
