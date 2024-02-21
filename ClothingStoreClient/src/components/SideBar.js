// Sidebar.js
import React from 'react';

const Sidebar = ({ onFilterByBrand, onFilterByType }) => {
  return (
    <div style={{ display: 'flex', flexDirection: 'column' }}>
      <div>
        <h2>Brands</h2>
        <ul style={{ listStyleType: 'none', padding: 0 }}>
          <li>
            <button onClick={() => onFilterByBrand("Nike")}>Nike</button>
          </li>
          <li>
            <button onClick={() => onFilterByBrand("Adidas")}>Adidas</button>
          </li>
          <li>
            <button onClick={() => onFilterByBrand("ZARA")}>ZARA</button>
          </li>
        </ul>
      </div>

      <div>
        <h2>Types</h2>
        <button onClick={() => onFilterByType("Jeans")}>Jeans</button>
        <button onClick={() => onFilterByType("Sneakers")}>Sneakers</button>
        <button onClick={() => onFilterByType("T-Shirt")}>Shirt</button>
        <button onClick={() => onFilterByType("Jackets")}>Jackets</button>
      </div>
    </div>
  );
};

export default Sidebar;
