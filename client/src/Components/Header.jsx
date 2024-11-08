import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import '../style/Header.css';

const Header = () => {
  const navigate = useNavigate();
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    if (localStorage.getItem('userName')) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);

  const handleLogout = async () => {
    localStorage.clear();
    setIsLoggedIn(false);
    await logout();
    navigate('/');
  };

  const logout = async () => {
    await fetch('/api/Auth/Logout', {
      method: 'POST',
      credentials: 'same-origin',
    });
  };

  return (
    <div className="main-header">
      <div className="header-buttons">
        <button onClick={() => navigate('/')}>Home</button>
        {!isLoggedIn ? <button onClick={() => navigate('/login')}>Log in</button> : <button onClick={handleLogout}>Log out</button>}
        {!isLoggedIn && <button onClick={() => navigate('/registration')}>Register</button>}
        {localStorage.getItem('role') === 'Admin' && <button onClick={() => navigate('/admin-page')}>Admin page</button>}

        <button onClick={() => navigate('/solar-watch')}>Solar Watch</button>
      </div>
      <div className="logged-in">
        {isLoggedIn && (
          <span>
            Logged in as <span className="user-name">{localStorage.getItem('userName')}</span>
          </span>
        )}
      </div>
    </div>
  );
};

export default Header;
