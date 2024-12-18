import { Navigate } from 'react-router-dom'

const ProtectedRoute = ({children}) => {

  if(!localStorage.getItem('userName')){
    return <Navigate to={'/login'}/>
  }
  return children; 
}

export default ProtectedRoute
