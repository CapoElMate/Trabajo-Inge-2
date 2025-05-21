import LoginReal from './components/LoginReal';
import {Routes, Route} from 'react-router-dom';
import Login from "./components/Login";
import HomePage from './components/HomePage';
import DetallePublicacion from './components/DetallePublicacion';
import SignUp from './components/SignUp';
import ForgotPassword from './components/ForgotPassword';
import ResetPassword from './components/ResetPassword';
function App(){
  return(
    <Routes>
      <Route path='/LoginReal' element={<LoginReal/>}/>
      <Route path='/HomePage' element={<HomePage/>}/>
      <Route path='/Login' element={<Login/>}/>
      <Route path='/SignUp' element={<SignUp/>}/>
      <Route path='/ForgotPassword' element={<ForgotPassword/>}/>
      <Route path='/DetallePublicacion/:id' element={<DetallePublicacion/>}/>
      <Route path='/ResetPassword' element={<ResetPassword/>}/>
    </Routes>
  );
}
export default App; 