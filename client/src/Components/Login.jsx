import { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import Header from './Header'

const Login = () => {
  const navigate = useNavigate()
  const [email, setEmail] = useState("admin@admin.com");
  const [password, setPassword] = useState("admin123");
  const [token, setToken] = useState("");

  const requestOptions = {
    method: 'POST',
    headers: {'Content-Type': 'application/json'},
    body: JSON.stringify({email, password}),
  }

  const handleLogin = async (e) => {
    e.preventDefault();
    const response = await fetch('/api/Auth/Login', requestOptions);
    if(response.ok){
      const data = await response.json();
      setToken(data.token);
      console.log(data);
      console.log(data.token);
    }
  }

  return (
    <div>
      <Header></Header>
      Login
      <form action="" onSubmit={handleLogin}>
        <label htmlFor="">Email</label>
        <input type="email" name="" id="" onChange={e => setEmail(e.target.value)} value={email}/>
        <label htmlFor="">Password</label>
        <input type="password" name="" id="" onChange={e => setPassword(e.target.value)} value={password}/>
      <button type='submit'>login</button>
      </form>
      <button onClick={() => navigate('/')}>home</button>
    </div>
  )
}

export default Login
