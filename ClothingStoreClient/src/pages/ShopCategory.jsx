import React, { useState, useEffect } from 'react';
import '../styles/ShopCategory.css';
import { getAllAdvertisements, FilterAdvertisementsByCategory } from '../http/AdvetisementApi';
import { useCategory } from '../context/CategoryContext';
import Sidebar from '../components/SideBar';
import AdvertisementList from '../components/AdvertismentList';
import womenBanner from '../assets/women_banner.jpg';
import menBanner from '../assets/man_banner.webp';
import allBanner from'../assets/all_banner.jpg';
import kidsBanner from '../assets/kids_banner.webp';

export default function ShopCategory() {
  const [advertisements, setAdvertisements] = useState([]);
  const [loading, setLoading] = useState(false);
  const { selectedCategory } = useCategory();
  const [selectedBrand, setSelectedBrand] = useState(null);
  const [selectedType, setSelectedType] = useState(null);

  useEffect(() => {
    if (selectedCategory === 'all') {
      fetchAllAdvertisements();
    } else {
      fetchAdvertisementsByCategory(selectedCategory);
    }
  }, [selectedCategory]);

  const fetchAllAdvertisements = async () => {
    try {
      setLoading(true);
      const response = await getAllAdvertisements();
      setAdvertisements(response);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching all advertisements:', error);
      setLoading(false);
    }
  };

  const fetchAdvertisementsByCategory = async (category) => {
    try {
      setLoading(true);
      const response = await FilterAdvertisementsByCategory(category);
      setAdvertisements(response);
      setLoading(false);
    } catch (error) {
      console.error('Error fetching advertisements by category:', error);
      setLoading(false);
    }
  };

  const filterByBrand = (brand) => {
    setSelectedBrand(brand);
  };

  const filterByType = (type) => {
    setSelectedType(type);
  };

  return (
    <div className="shop-container">
      <div className="sidebar-container">
      </div>
      <div className="advertisement-list-container">
        {selectedCategory === 'women' && (
          <div className="banner">
            <img src={womenBanner} alt="Women Banner" className="banner-image" />
          </div>
        )}
        {selectedCategory === 'men' && (
          <div className="banner">
            <img src={menBanner} alt="Men Banner" className="banner-image" />
          </div>
        )}
        {selectedCategory === 'kids' && (
          <div className="banner">
            <img src={kidsBanner} alt="Kids Banner" className="banner-image" />
          </div>
        )}
        {selectedCategory === 'all' && (
          <div className="banner">
            <img src={allBanner} alt="All Banner" className="banner-image" />
          </div>
        )}
       
        <AdvertisementList advertisements={advertisements} selectedBrand={selectedBrand} selectedType={selectedType} />
      </div>
    </div>
  );
}
