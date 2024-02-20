import { createContext, useContext, useState, useEffect } from 'react';
import { jwtDecode as jwt_decode } from 'jwt-decode';

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  
  const [userId, setUserId] = useState(null);
  const [token, setToken] = useState(() => {
    const storedToken = localStorage.getItem('authToken');
    return storedToken || null;
  });

  useEffect(() => {
    const extractUserInfoFromToken = (token) => {
      try {
        const decodedToken = jwt_decode(token);
        console.log('Decoded Token:', decodedToken);

      
        const userId = decodedToken ? decodedToken.UserId : null;
        console.log('User ID:', userId);

        setUserId(userId);
      } catch (error) {
        console.error('Error decoding token:', error);
      }
    };

    if (token) {
      extractUserInfoFromToken(token);
    } else {
      setUserId(null);
    }
  }, [token]);

  const handleLogin = (newToken) => {
    setToken(newToken);
    localStorage.setItem('authToken', newToken);
    console.log('Token set in localStorage:', newToken);
  };

  const handleLogout = () => {
    setToken(null);
    setUserId(null);
    localStorage.removeItem('authToken');
    console.log('Token removed from localStorage.');
    window.location.reload();
  };

  return (
    <AuthContext.Provider value={{ token, setToken, userId, handleLogin, handleLogout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  return useContext(AuthContext);
};
