import { Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import HomePage from './components/HomePage';
import { AuthProvider } from './AuthContext';
import SignUp from './components/SignUp';
import ForgotPassword from './components/ForgotPassword';
import ValidateUser from './components/ValidateUser';
import PostDetail from './components/PostDetail';
import Header from './components/Header';
import HomePageAdmin from './components/HomePageAdmin';
import './components/App.css';
import 'bootstrap/dist/css/bootstrap.min.css'
import Leases from './components/Leases';
import Rentals from './components/Rentals';
import ResetPassword from './components/ResetPassword';
import PermitFileUploader from './components/PermitFileUploader';
import Profile from './components/Profile';
import ValidatePermit from './components/ValidatePermit';
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
        <Route path='/HomePageAdmin' element={<HomePageAdmin/>} />
        <Route path='/Leases' element={<Leases/>}/>
        <Route path='/Rentals' element={<Rentals/>}/>      
        <Route path='/Profile' element={<Profile/>}/>
        <Route path='/ResetPassword' element={<ResetPassword/>}/>
        <Route path='/PermitFileUploader' element={<PermitFileUploader/>}/>
        <Route path='/ValidatePermit' element={<ValidatePermit/>}/>
      
      </Routes>
    </AuthProvider>
  );
};

export default App;
