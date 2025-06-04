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
import ForgotPassword from "./components/ForgotPassword";
import Rentals from "./components/Rentals";

import CrearPublicacion from "./components/Publicacion/CrearPublicacion";
import ModificarPublicacion from "./components/Publicacion/ModificarPublicacion";

import CrearMaquinaria from "./components/Maquinaria/CrearMaquinaria";
import EditarMaquinaria from "./components/Maquinaria/EditarMaquinaria";
import ListarMaquinaria from "./components/Maquinaria/ListarMaquinaria";

import DetalleReserva from "./components/Reserva/DetalleReserva";
import DetalleAlquiler from "./components/Alquiler/DetalleAlquiler";

import Perfil from "./components/Perfil/Perfil";
import ResetPassword from "./components/ResetPassword";

function App() {
  return (
    <AuthProvider>
      <Routes>
         <Route path="/" element={<Navigate to="/HomePage" />} />
        <Route path="/resetPassword" element={<ResetPassword/>}/>
        <Route path="/login" element={<Login />} />
        <Route path="/noautorizado" element={<NoAutorizado />} />
        <Route path="/HomePage" element={<HomePage />} />
        <Route
          path="/DetallePublicacion/:id"
          element={<DetallePublicacion />}
        />
        <Route path="/SignUp" element={<SignUp />} />
        <Route path="/ForgotPassword" element={<ForgotPassword />} />
        <Route path="/Rentals" element={<Rentals />} />
        <Route path="/DetalleReserva/:id" element={<DetalleReserva />} />
        <Route path="/DetalleAlquiler/:id" element={<DetalleAlquiler />} />
        <Route path="/Perfil" element={<Perfil />} />

        {/* Cliente */}
        <Route element={<PrivateRoute rolesPermitidos={["Cliente"]} />}></Route>

        {/* Dueño */}
        <Route element={<PrivateRoute rolesPermitidos={["Dueño"]} />}>
          <Route path="/HomePageAdmin" element={<HomePageAdmin />} />
          <Route path="/CrearPublicacion" element={<CrearPublicacion />} />
          <Route
            path="/ModificarPublicacion/:id"
            element={<ModificarPublicacion />}
          />
          <Route path="/HomePage" element={<HomePage />} />
          <Route path="/CrearMaquinaria" element={<CrearMaquinaria />} />
          <Route
            path="/ModificarMaquinaria/:id"
            element={<EditarMaquinaria />}
          />
          <Route path="/ListarMaquinaria" element={<ListarMaquinaria />} />
        </Route>

        {/* Emleado */}
        <Route element={<PrivateRoute rolesPermitidos={["Empleado"]} />}>
          <Route path="/EmployeeHome" element={<EmployeeHome />} />
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
