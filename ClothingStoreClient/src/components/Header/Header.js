import React, { useState } from 'react';
import logo from '../../assets/shoplogo.jpg'; 
import { Link } from 'react-router-dom';
import './Header.css'; 

export default function Header() {
  const [menu, setMenu] = useState("shop");

  return (
    <div className='navbar'>
      <div className='nav-logo'>
        <img src={logo} alt="Logo" />
      </div>
      <ul className='nav-menu'>
        <li onClick={() => setMenu("shop")}><Link  style = {{textDecoration:'none', color:'black'}} to ='/'>Shop</Link>{menu==="shop" && <hr/>}</li>
        <li onClick={() => setMenu("mens")}><Link  style = {{textDecoration:'none', color:'black'}}to ='/mens'>Men</Link>{menu==="mens" && <hr/>}</li>
        <li onClick={() => setMenu("womens")}><Link  style = {{textDecoration:'none', color:'black'}}to ='/womens'>Women</Link>{menu==="womens" && <hr/>}</li>
        <li onClick={() => setMenu("kids")}><Link style = {{textDecoration:'none', color:'black'}} to ='/kids'>Kids</Link>{menu==="kids" && <hr/>}</li>
      </ul>
      <div className='nav-login-cart'>
        <Link to ='/login'><button>Login</button></Link>
        <i className="fas fa-shopping-cart"></i> 
        <div className='nav-cart-count'>0</div>
      </div>
    </div>
  );
}
