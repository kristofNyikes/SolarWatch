import { useState } from 'react';
import Header from './Header';
import WelcomeNewUser from './WelcomeNewUser';

const Registration = () => {
  const [email, setEmail] = useState("");
  const [userName, setUserName] = useState("");
  const [password, setPassword] = useState("");
  const [registeredData, setRegisteredData] = useState(null);

  const requestOptions = {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, userName, password })
  };

  const handleRegistration = async (e) => {
    e.preventDefault();
    console.log("email:", email);
    console.log("userName", userName);
    console.log(("password"), password);
    console.log("JSON.stringify: ", JSON.stringify({ email, userName, password }))
    const response = await fetch('/api/Auth/Register', requestOptions);
    if (response.ok) {
      const data = await response.json();
      console.log(data);
      setRegisteredData(data);
    }
  };

  return (
    <div>
      <Header />
      {!registeredData ? (
        <div>
          <h2>Registration</h2>
          <form onSubmit={handleRegistration}>
            <label htmlFor="username">Username</label>
            <input 
              type="text" 
              id="username" 
              onChange={(e) => setUserName(e.target.value)} 
              value={userName} 
            />
            <label htmlFor="email">Email</label>
            <input 
              type="email" 
              id="email" 
              onChange={(e) => setEmail(e.target.value)} 
              value={email} 
            />
            <label htmlFor="password">Password</label>
            <input 
              type="password" 
              id="password" 
              onChange={(e) => setPassword(e.target.value)} 
              value={password} 
            />
            <button type="submit">Register</button>
          </form>
        </div>
      ) : (
        // Render WelcomeNewUser after successful registration
        <WelcomeNewUser data={registeredData} />
      )}
    </div>
  );
};

export default Registration;