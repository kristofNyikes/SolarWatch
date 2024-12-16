import { Navigate } from 'react-router-dom'

const ProtectedRoute = ({children}) => {
  // const token = localStorage.getItem('authToken');

  if(!localStorage.getItem('userName')){
    return <Navigate to={'/login'}/>
  }
  return children; 
}

export default ProtectedRoute
