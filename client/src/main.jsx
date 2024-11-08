import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import App from './App.jsx';
import { createBrowserRouter, RouterProvider } from 'react-router-dom';
import Registration from './Components/Registration.jsx';
import Login from './Components/Login.jsx';
import SolarWatch from './Components/SolarWatch.jsx';
import ProtectedRoute from './Components/ProtectedRoute.jsx';
import Admin from './Components/Admin.jsx';
import './index.css'

const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
  },
  {
    path: '/registration',
    element: <Registration />,
  },
  {
    path: '/login',
    element: <Login />,
  },
  {
    path: '/solar-watch',
    element: (
      <ProtectedRoute>
        <SolarWatch />
      </ProtectedRoute>
    ),
  },
  {
    path: '/admin-page',
    element: (
      <Admin/>
    ),
  },
]);

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router} />
  </StrictMode>
);
