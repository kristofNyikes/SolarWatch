import './App.css'
import { useNavigate } from 'react-router-dom'
import Header from './Components/Header';

function App() {
  const navigate = useNavigate();
  return (
    <>
      <Header/>
      <div>
        <h1>Welcome to solarwatch</h1>
        <button type='button' onClick={() => navigate("/registration")}>Register</button>
        <button type='button' onClick={() => navigate("/login")}>Login</button>
      </div>
    </>
  )
}

export default App
