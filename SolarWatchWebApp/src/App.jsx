import './App.css';
import { useNavigate } from 'react-router-dom';
import Header from './Components/Header';

function App() {
  const navigate = useNavigate();

  return (
    <>
      <Header />
      <div className="main">
        {!localStorage.getItem('userName') ? (
          <>
            <div className="welcome-message">
              <h1>Welcome to solarwatch</h1>
            </div>
            <div className="navigation-buttons">
              <button type="button" onClick={() => navigate('/registration')}>
                Register
              </button>
              <button type="button" onClick={() => navigate('/login')}>
                Login
              </button>
            </div>
          </>
        ) : (
          <div className="logged-in-message">
            <p>
              Thank you for using Solarwatch dear <span className="user-name">{localStorage.getItem('userName')} </span>
            </p>
          </div>
        )}
      </div>
    </>
  );
}

export default App;
