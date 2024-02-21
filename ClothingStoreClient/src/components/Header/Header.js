import React, { useState } from 'react';
import { Link } from 'react-router-dom'; // импортируем Link
import logo from '../../assets/shoplogo.jpg'; 
import { useLocation } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext'; 
import { useCategory } from '../../context/CategoryContext';
import './Header.css'; 

export default function Header() {
  const { token, handleLogout } = useAuth();
  const { setSelectedCategory } = useCategory(); 
  const location = useLocation(); 

  const handleCategorySelect = (category) => {
    setSelectedCategory(category); 
  };

  return (
    <div className='navbar'>
      <div className='nav-logo'>
        <img src={logo} alt="Logo" />
      </div>
      <ul className='nav-menu'>
        <li className={location.pathname === '/' ? 'active' : ''} onClick={() => handleCategorySelect("shop")}>
          <Link style={{textDecoration:'none', color:'black'}} to='/'>Shop</Link>
          <hr className={location.pathname === '/' ? 'active' : ''} />
        </li>
        <li className={location.pathname === '/all' ? 'active' : ''} onClick={() => handleCategorySelect("all")}>
          <Link style={{textDecoration:'none', color:'black'}} to='/all'>All</Link>
          <hr className={location.pathname === '/all' ? 'active' : ''} />
        </li>
        <li className={location.pathname === '/men' ? 'active' : ''} onClick={() => handleCategorySelect("men")}>
          <Link style={{textDecoration:'none', color:'black'}} to='/men'>Men</Link>
          <hr className={location.pathname === '/men' ? 'active' : ''} />
        </li>
        <li className={location.pathname === '/women' ? 'active' : ''} onClick={() => handleCategorySelect("women")}>
          <Link style={{textDecoration:'none', color:'black'}} to='/women'>Women</Link>
          <hr className={location.pathname === '/women' ? 'active' : ''} />
        </li>
        <li className={location.pathname === '/kids' ? 'active' : ''} onClick={() => handleCategorySelect("kids")}>
          <Link style={{textDecoration:'none', color:'black'}} to='/kids'>Kids</Link>
          <hr className={location.pathname === '/kids' ? 'active' : ''} />
        </li>
      </ul>
      <div className='nav-login-cart'>
        {token ? (
          <button onClick={handleLogout}>Logout</button>
        ) : (
          <Link to='/login'><button>Login</button></Link>
        )}
        <Link to='/create'> 
          <i className="fas fa-plus"></i> 
        </Link>
        <i className="fas fa-shopping-cart"></i> 
        <div className='nav-cart-count'>0</div>
      </div>
    </div>
  );
}
