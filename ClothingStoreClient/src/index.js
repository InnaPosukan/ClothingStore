import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import { AuthProvider } from './context/AuthContext';
import { CategoryProvider } from './context/CategoryContext';
import { CartProvider } from './context/CartContex';
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
  <AuthProvider>
  <CartProvider>

  <CategoryProvider>

        <App />

        </CategoryProvider>
        </CartProvider>

      </AuthProvider>
   </React.StrictMode>
);
