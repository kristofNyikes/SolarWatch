/* eslint-disable react/prop-types */
import { useNavigate } from 'react-router-dom';
import { useEffect } from 'react';

const WelcomeNewUser = ({ data }) => {
  const navigate = useNavigate();

  useEffect(() => {
    const redirectInSeconds = (sec, redirectTo) => {
      setTimeout(() => {
        navigate(redirectTo);
      }, sec * 1000);
    };

    if (!data.userName) {
      redirectInSeconds(3, "/");
    } else {
      redirectInSeconds(4, '/login');
    }
  }, [data, navigate]);

  return (
    <div>
      {data.userName ? (
        <>
          <h2>Welcome {data.userName}!</h2>
          <p>Thank you for your registration.</p>
          <p>You will be redirected to the login page shortly.</p>
        </>
      ) : (
        <p>Something went wrong</p>
      )}
    </div>
  );
};

export default WelcomeNewUser;