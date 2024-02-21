import React from 'react';
import { Link } from 'react-router-dom'; // Import Link component
import { BASE_URL } from '../utils/apiConfig';

const AdvertisementList = ({ advertisements, selectedBrand, selectedType, selectedColor }) => {
  const filteredAdvertisements = advertisements.filter(advertisement => {
    if (selectedBrand && selectedType && selectedColor) {
      return advertisement.advertisementAttributes.some(attribute =>
        attribute.brand === selectedBrand && attribute.type === selectedType && attribute.color === selectedColor
      );
    } else if (selectedBrand && selectedType) {
      return advertisement.advertisementAttributes.some(attribute =>
        attribute.brand === selectedBrand && attribute.type === selectedType
      );
    } else if (selectedBrand && selectedColor) {
      return advertisement.advertisementAttributes.some(attribute =>
        attribute.brand === selectedBrand && attribute.color === selectedColor
      );
    } else if (selectedType && selectedColor) {
      return advertisement.advertisementAttributes.some(attribute =>
        attribute.type === selectedType && attribute.color === selectedColor
      );
    } else if (selectedBrand) {
      return advertisement.advertisementAttributes.some(attribute => attribute.brand === selectedBrand);
    } else if (selectedType) {
      return advertisement.advertisementAttributes.some(attribute => attribute.type === selectedType);
    } else if (selectedColor) {
      return advertisement.advertisementAttributes.some(attribute => attribute.color === selectedColor);
    }
    return true;
  });

  return (
    <div className="advertisement-list">
      {filteredAdvertisements.map(advertisement => (
        <Link to={`/advertisement/${advertisement.adId}`} key={advertisement.adId}> 
          <div className="advertisement-container">
            <div className="advertisement-card">
              <img src={`${BASE_URL}/images/${advertisement.imagePath}`} className="advertisement-image" alt="Advertisement" />
              <div className="advertisement-details">
                <p>{advertisement.price} $</p>
                <button className="buy-button">Buy</button>
              </div>
              <div className="desc">
                <p>{advertisement.description} </p>

              </div>
            </div>
          </div>
        </Link>
      ))}
    </div>
  );
};

export default AdvertisementList;
