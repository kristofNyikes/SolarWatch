import { useNavigate } from 'react-router-dom'
const Header = () => {
  const navigate = useNavigate();

  return (
    <div>
      <button onClick={() => navigate('/')}>Home</button>
      <button onClick={() => navigate('/login')}>Login</button>
      <button onClick={() => navigate('/registration')}>Register</button>
      <button onClick={() => navigate('/solar-watch')}>Solar Watch</button>
    </div>
  )
}

export default Header
