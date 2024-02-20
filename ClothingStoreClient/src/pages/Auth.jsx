import React, { useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { authenticate } from '../http/AuthApi';
import '../styles/Auth.css'

const Auth = () => {
  const { handleLogin } = useAuth();
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [firstName, setFirstName] = useState('');
  const [lastName, setLastName] = useState('');
  const [isLoginMode, setLoginMode] = useState(true);
  const [showPassword, setShowPassword] = useState(false);

  const handleToggleMode = () => {
    setLoginMode((prevMode) => !prevMode);
  };

  const handleTogglePasswordVisibility = () => {
    setShowPassword((prevShowPassword) => !prevShowPassword);
  };
  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const responseData = await authenticate(isLoginMode, email, password, phoneNumber, firstName, lastName);

      const token = responseData.token;
      handleLogin(token);

      console.log(`${isLoginMode ? 'Login' : 'Registration'} successful. Token:`, token);
      window.alert(`${isLoginMode ? 'Login' : 'Registration'} successful`);

      if (isLoginMode) {
        window.location.replace('/');
      } else {
        window.location.replace('/login');
      }
    } catch (error) {
      console.error(`Error during ${isLoginMode ? 'login' : 'registration'}:`, error);
      window.alert(`Error during ${isLoginMode ? 'login' : 'registration'}: ${error.message}`);
    }
  };

  return (
    <div className='auth-container'>
      <h2 className="auth-title">{isLoginMode ? 'Login' : 'Registration'}</h2>
      <form className="auth-form" onSubmit={handleSubmit}>
        <div className="form-group">
          <label className="form-label" htmlFor="email">Email:</label>
          <input
            className="form-input"
            type="text"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </div>
        <div className="form-group">
          <label className="form-label" htmlFor="password">Password:</label>
          <div className="password-input-container">
            <input
              className="form-input"
              type={showPassword ? 'text' : 'password'}
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <i
              className={`eye-icon ${showPassword ? 'fas fa-eye-slash' : 'fas fa-eye'}`}
              onClick={handleTogglePasswordVisibility}
            ></i>
          </div>
        </div>
        {!isLoginMode && (
          <>
            <div className="form-group">
              <label className="form-label" htmlFor="phoneNumber">Phone Number:</label>
              <input
                className="form-input"
                type="number"
                id="phoneNumber"
                value={phoneNumber}
                onChange={(e) => setPhoneNumber(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label className="form-label" htmlFor="firstName">First Name:</label>
              <input
                className="form-input"
                type="text"
                id="firstName"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
              />
            </div>
            <div className="form-group">
              <label className="form-label" htmlFor="lastName">Last Name:</label>
              <input
                className="form-input"
                type="text"
                id="lastName"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
              />
            </div>
          </>
        )}
        <button className="form-button" type="submit">{isLoginMode ? 'Login' : 'Register'}</button>
      </form>
      <p className="form-toggle" onClick={handleToggleMode}>
        {isLoginMode ? 'Don\'t have an account?' : 'Already have an account?'}
      </p>
    </div>
  );
};

export default Auth;
