import React, { useState, useEffect } from 'react';
import { getAllAdvertisementsByUser, deleteAdvertisement  } from '../http/AdvetisementApi';
import { useAuth } from '../context/AuthContext';
import { BASE_URL } from '../utils/apiConfig';
import '../styles/MyOrders.css'; 

export default function MyOrders() {
  const { userId } = useAuth();
  const [advertisements, setAdvertisements] = useState([]);

  useEffect(() => {
    const fetchAdvertisements = async () => {
      try {
        const data = await getAllAdvertisementsByUser(userId);
        setAdvertisements(data);
      } catch (error) {
        console.error('Error fetching advertisements:', error);
      }
    };

    fetchAdvertisements();
  }, [userId]); 

  const handleDeleteAdvertisement = async (advertisementId) => {
    try {
      await deleteAdvertisement(advertisementId);
      
      const updatedAdvertisements = advertisements.filter(advertisement => advertisement.adId !== advertisementId);
      setAdvertisements(updatedAdvertisements);

      // Display an alert after successful deletion
      alert('Advertisement successfully deleted.');
    } catch (error) {
      console.error('Error deleting advertisement:', error);
    }
  };

  return (
    <div className="my-orders-container">
      <h2 className="my-orders-title">My Orders</h2>
      <div className='my-info'></div>
      <div className="adv-list">
        {advertisements.map(advertisement => (
          <div key={advertisement.adId} className="adv-item">
            <div className="adv-content">
              <img src={`${BASE_URL}/images/${advertisement.imagePath}`} className="adv-img" alt="Advertisement" />
            </div>
            <div className="advert-details">
              <h3 className="adv-title">Title: {advertisement.title}</h3>
              <p className="adv-description">Description: {advertisement.description}</p>
              <p className="adv-price">Price: {advertisement.price}$</p>
              <p className="adv-date">Publication Date: {advertisement.publicationDate}</p>
              
              {advertisement.advertisementAttributes.map((attribute) => (
                <span key={attribute.id} className="advertisement-attribute">
                  <span className="attribute-circle">
                    Brand: {attribute.brand}
                  </span>
                  <span className="attribute-circle">
                    Size: {attribute.size}
                  </span>
                  <span className="attribute-circle">
                    Color: {attribute.color}
                  </span>
                  <span className="attribute-circle">
                    Type: {attribute.type}
                  </span>
                </span>
              ))}
             <button className="delete-button" onClick={() => handleDeleteAdvertisement(advertisement.adId)}>Delete</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
