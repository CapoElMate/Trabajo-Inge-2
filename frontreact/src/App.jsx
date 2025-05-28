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
import CargarMaquinaria from './components/CargarMaquinaria';
import EditarMaquinaria from './components/EditarMaquinaria';
import MaquinariaDetail from './components/DetalleMaquinaria';
import MaquinariaList from './components/MaquinariaList';

const App = () => {
  return (
    <AuthProvider>
      <Routes>
        <Route path="/Login" element={<Login />} />
        <Route path="/HomePage" element={<HomePage />} />
        <Route path="/ForgotPassword" element={<ForgotPassword />} />
        <Route path="/SignUp" element={<SignUp />} />
        <Route path='/ValidateUser' element={<ValidateUser />}/> 
        <Route path='/PostDetail/:id' element={<PostDetail />}/> 
        <Route path='/Header' element={<Header/>} />
        <Route path='/CargarPublicacion' element={<CargarPublicacion/>} />
        <Route path='/CargarMaquinaria' element={<CargarMaquinaria/>} />
         <Route path="/modificar/:id" element={<EditarMaquinaria />} />
        <Route path="/maquinaria/:id" element={<MaquinariaDetail />} />
        <Route path='/maquinarias' element={<MaquinariaList/>}/>
      </Routes>
    </AuthProvider>
  );
};

export default App;
