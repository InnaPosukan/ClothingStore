import React, { useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { useAuth } from '../../context/AuthContext';
import { useCategory } from '../../context/CategoryContext';
import { useCart } from '../../context/CartContex';
import './Header.css';
import logo from '../../assets/shoplogo.jpg'; 
import { BASE_URL } from '../../utils/apiConfig';

export default function Header({ onOrderClick }) {
  const { token, handleLogout } = useAuth();
  const { setSelectedCategory } = useCategory();
  const [showCartModal, setShowCartModal] = useState(false);
  const location = useLocation();
  const { cartItems, setCartItems } = useCart();

  const handleCategorySelect = (category) => {
    setSelectedCategory(category);
  };

  const toggleCartModal = () => {
    setShowCartModal(!showCartModal);
  };

  const addToCart = (item) => {
    const newItem = { ...item, quantity: 1, totalPrice: item.price };
    setCartItems([...cartItems, newItem]);
  };

  const removeFromCart = (indexToRemove) => {
    setCartItems(cartItems.filter((_, index) => index !== indexToRemove));
  };

  const handleQuantityChange = (e, index) => {
    const newQuantity = parseInt(e.target.value);
    const newCartItems = [...cartItems];
    newCartItems[index] = { ...newCartItems[index], quantity: newQuantity, totalPrice: newQuantity * newCartItems[index].price };
    setCartItems(newCartItems);
  };

  const totalCartPrice = cartItems.reduce((total, item) => total + (item.totalPrice || 0), 0);

  return (
    <div className='navbar'>
      <div className='nav-logo'>
        <Link to="/">
          <img src={logo} alt="Logo" />
        </Link>
      </div>
      <ul className='nav-menu'>
        {[ 'all', 'men', 'women', 'kids'].map((category) => (
          <li key={category} className={location.pathname === `/${category}` ? 'active' : ''} onClick={() => handleCategorySelect(category)}>
            <Link to={`/${category}`} style={{ textDecoration: 'none', color: 'black' }}>{category.toUpperCase()}</Link>
            <hr className={location.pathname === `/${category}` ? 'active' : ''} />
          </li>
        ))}
      </ul>
      <div className='nav-login-cart'>
        {token ? (
          <button onClick={handleLogout}>Logout</button>
        ) : (
          <Link to='/login'><button>Login</button></Link>
        )}
        <Link to='/create'><i className="fas fa-plus"></i></Link>
        <Link to='/profile'><i className="fas fa-user"></i></Link>

        <i className="fas fa-shopping-cart" onClick={toggleCartModal}></i>
        <div className='nav-cart-count'>{cartItems.length}</div>
      </div>
      {showCartModal && (
        <div className="modal">
          <div className="modal-content">
            <span className="close" onClick={toggleCartModal}>&times;</span>
            <table className="cart-table">
              <thead>
                <tr>
                  <th>Item</th>
                  <th>Price</th>
                  <th>Quantity</th>
                  <th>Action</th>
                </tr>
              </thead>
              <tbody>
                {cartItems.map((item, index) => (
                  <tr key={index}>
                    <td>
                      <div className="cart-item-container">
                        <div className="img-container">
                          <img src={`${BASE_URL}/images/${item.imagePath}`} className="advertisement-img" alt="Advertisement" />
                        </div>
                        <div className="item-details">
                          <p>{item.title}</p>
                        </div>
                      </div>
                    </td>
                    <td>${item.totalPrice}</td>
                    <td>
                      <input
                        type="number"
                        min="1"
                        value={item.quantity}
                        onChange={(e) => handleQuantityChange(e, index)}
                        className="quantity-input" 
                      />
                    </td>
                    <td>
                      <button className='remove-btn' onClick={() => removeFromCart(index)}>Remove</button>
                    </td>
                  </tr>
                ))}
                <tr>
                  <td colSpan="3">Total: ${totalCartPrice}</td>
                  <td colSpan="3" className="order-btn-cell">
                    <Link to={{
                      pathname: '/order',
                      state: { cartItems: cartItems } 
                    }} className='order-btn' onClick={() => setShowCartModal(false)}>To Order</Link>
                  </td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  );
}
