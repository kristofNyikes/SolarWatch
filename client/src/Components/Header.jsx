import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom'
const Header = () => {
  const navigate = useNavigate();
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem('authToken');
    if(token){
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, [])

  const handleLogout = () => {
    localStorage.clear();
    setIsLoggedIn(false);
    navigate('/');
  }

  return (
    <div>
      <button onClick={() => navigate('/')}>Home</button>
      {
        !isLoggedIn ? (
          <button onClick={() => navigate('/login')}>Log in</button>
        )
        :
        (<button onClick={handleLogout}>Log out</button>)
      }
      {
        !isLoggedIn && <button onClick={() => navigate('/registration')}>Register</button>
      }
      
      <button onClick={() => navigate('/solar-watch')}>Solar Watch</button>
      <div>
        {isLoggedIn ? 
        (<span>logged in as {localStorage.getItem('userName')}</span>) : 
        (<span>not logged in</span>)}
      </div>
    </div>
  )
}

export default Header
