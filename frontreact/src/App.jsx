import LoginReal from './components/LoginReal';
import {Routes, Route} from 'react-router-dom';
import SignUp from './components/SignUp';
function App(){
  return(
    <Routes>
      <Route path='/LoginReal' element={<LoginReal/>}/>
      <Route path='/SignUp' element={<SignUp/>}/>
    </Routes>
  );
}
export default App; 