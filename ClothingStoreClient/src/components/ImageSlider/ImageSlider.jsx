import React from 'react';
import Slider from 'react-slick';
import 'slick-carousel/slick/slick.css';
import 'slick-carousel/slick/slick-theme.css';
import banner1 from '../../assets/banner1.webp';
import banner2 from '../../assets/banner2.webp';
import './ImageSlider.css'; 

export default function ImageSlider() {
  const settings = {
    dots: false,
    infinite: true,
    speed: 900,
    slidesToShow: 1,
    slidesToScroll: 1,
    autoplay: true, 
    autoplaySpeed: 3000,
    responsive: [
      {
        breakpoint: 768,
        settings: {
          slidesToShow: 1,
        }
      }
    ]
  };

  return (
    <div>
      <div className="image-slider-container">
        <Slider {...settings}>
          <div>
            <img src={banner1} alt="First Image" className="image-slider-image" />
          </div>
          <div>
            <img src={banner2} alt="Second Image" className="image-slider-image" />
          </div>
        </Slider>
      </div>
    </div>
  );
}
