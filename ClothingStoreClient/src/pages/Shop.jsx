import React from 'react';
import ImageSlider from '../components/ImageSlider/ImageSlider';
import jeans from '../assets/jeans.webp'; 
import tshirt from '../assets/t-shirt.webp'; 
import dress from '../assets/dress.webp'; 
import sport_suit from '../assets/sports_suit.jpg'; 
import suit from '../assets/suit.webp'; 

import '../styles/Shop.css'; 

export default function Shop() {

  return (
    <div className='shop-container'>
      <div className='image-slider'>
        <ImageSlider/> 
      </div>
      <div className='category-container'>
        <div className='category-title'>Our Category</div>
        <div className='image-grid'>
          <div className='image-wrapper'>
            <img src={jeans} alt="Jeans" className="image" />
            <div className='image-caption'>Jeans</div>
          </div>
          <div className='image-wrapper'>
            <img src={tshirt} alt="T-Shirt" className="image" />
            <div className='image-caption'>T-Shirt</div>
          </div>
          <div className='image-wrapper'>
            <img src={dress} alt="Dress" className="image" />
            <div className='image-caption'>Dress</div>
          </div>
          <div className='image-wrapper'>
            <img src={sport_suit} alt="Sport-Suit" className="image" />
            <div className='image-caption'>Sport Suit</div>
          </div>
          <div className='image-wrapper'>
            <img src={suit} alt="Suit" className="image" />
            <div className='image-caption'>Suit</div>
          </div>
        </div>
      </div>
    </div>
  );
}
