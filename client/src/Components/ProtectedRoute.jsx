import { Navigate } from 'react-router-dom'

const ProtectedRoute = ({children}) => {
  const token = localStorage.getItem('authToken')

  if(!token){
    return <Navigate to={'/login'}/>
  }
  return children; 
}

export default ProtectedRoute
