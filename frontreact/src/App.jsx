import { Routes, Route } from 'react-router-dom';
import Login from './components/Login';
import HomePage from './components/HomePage';
import { AuthProvider } from './AuthContext';
import SignUp from './components/SignUp';
import ForgotPassword from './components/ForgotPassword';
import ValidateUser from './components/ValidateUser';

const App = () => {
  return (
    <AuthProvider>
      <Routes>
        <Route path="/Login" element={<Login />} />
        <Route path="/homePage" element={<HomePage />} />
        <Route path="/ForgotPassword" element={<ForgotPassword />} />
        <Route path="/SignUp" element={<SignUp />} />
        <Route path='/ValidateUser' element={<ValidateUser />}/> 
      </Routes>
    </AuthProvider>
  );
};

export default App;
