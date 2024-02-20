import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import '../styles/ShopCategory.css';
import { getAllAdvertisements, FilterAdvertisementsByCategory } from '../http/AdvetisementApi';
import { useCategory } from '../context/CategoryContext';
import { BASE_URL } from '../utils/apiConfig';


export default function ShopCategory() {
  const [advertisements, setAdvertisements] = useState([]);
  const { selectedCategory } = useCategory();

  useEffect(() => {
    if (selectedCategory) {
      fetchAdvertisementsByCategory(selectedCategory);
    } else {
      fetchAdvertisements();
    }
    console.log("Selected category:", selectedCategory);
  }, [selectedCategory]);

  const fetchAdvertisements = async () => {
    try {
      const response = await getAllAdvertisements();
      setAdvertisements(response);
    } catch (error) {
      console.error('Error fetching advertisements:', error);
    }
  };

  const fetchAdvertisementsByCategory = async (category) => {
    try {
      const response = await FilterAdvertisementsByCategory(category);
      setAdvertisements(response);
    } catch (error) {
      console.error('Error fetching advertisements by category:', error);
    }
  };

  return (
    <div>
      <Link to="/create">Create Advertisement</Link>
      <h2>All Advertisements</h2>
      <p>Selected category: {selectedCategory}</p>
      <div>
        {advertisements.map(advertisement => (
          <div key={advertisement.adId}>
            <h3>{advertisement.title}</h3>
            <p>{advertisement.description}</p>
            <p>{advertisement.price}</p>
            <img src={`${BASE_URL}/images/${advertisement.imagePath}`} alt="Advertisement" />
          </div>
        ))}
      </div>
    </div>
  );
}
