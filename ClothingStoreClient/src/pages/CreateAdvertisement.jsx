import React, { useState } from 'react';
import { createAdvertisement } from '../http/AdvetisementApi';
import { useAuth } from '../context/AuthContext';
import '../styles/CreateAdvertisement.css';

export default function CreateAdvertisement() {
  const { userId } = useAuth();
  const [formData, setFormData] = useState({
    title: '',
    description: '',
    price: '',
    size: '',
    color: '',
    brand: '',
    type: '',
    category: '',
    image: null,
    imageUrl: null,
    sellerId: userId
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prevState => ({
      ...prevState,
      [name]: value
    }));
  };

  const handleImageChange = (e) => {
    const file = e.target.files[0];
    setFormData(prevState => ({
      ...prevState,
      image: file,
      imageUrl: URL.createObjectURL(file)
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      console.log('Data sent to server:', formData);
      await createAdvertisement(formData);
      setFormData({
        title: '',
        description: '',
        price: '',
        size: '',
        color: '',
        brand: '',
        type: '',
        image: null,
        imageUrl: null,
        category: '',
        SellerId: userId
      });
    } catch (error) {
      console.error('Error creating advertisement:', error);
    }
  };

  return (
    <div className="create-advertisement-form">
      <h2>Create Advertisement</h2>
      <form onSubmit={handleSubmit} className="advertisement-form">
        <div className="form-group">
          <label>Title:</label>
          <input type="text" name="title" value={formData.title} onChange={handleChange} required className="form-control" />
        </div>
        <div className="form-group">
          <label>Description:</label>
          <textarea name="description" value={formData.description} onChange={handleChange} required className="form-control" />
        </div>
        <div className="form-group">
          <label>Category:</label>
          <select name="category" value={formData.category} onChange={handleChange} className="form-control">
            <option value="">Select Category</option>
            <option value="men">Men</option>
            <option value="women">Women</option>
            <option value="kids">Kids</option>
          </select>
        </div>
        <div className="form-group">
          <label>Size:</label>
          <select name="size" value={formData.size} onChange={handleChange} className="form-control">
            <option value="">Select Size</option>
            <option value="xs">XS</option>
            <option value="s">S</option>
            <option value="m">M</option>
            <option value="l">L</option>
            <option value="xl">XL</option>
            <option value="xxl">XXL</option>
          </select>
        </div>
        <div className="form-group">
          <label>Color:</label>
          <select name="color" value={formData.color} onChange={handleChange} className="form-control">
            <option value="">Select Color</option>
            <option value="red">Red</option>
            <option value="blue">Blue</option>
            <option value="green">Green</option>
            <option value="yellow">Yellow</option>
            <option value="orange">Orange</option>
          </select>
        </div>
        <div className="form-group">
          <label>Brand:</label>
          <select name="brand" value={formData.brand} onChange={handleChange} className="form-control">
            <option value="">Select Brand</option>
            <option value="Nike">Nike</option>
            <option value="Adidas">Adidas</option>
            <option value="Puma">Puma</option>
            <option value="Reebok">Reebok</option>
          </select>
        </div>
        <div className="form-group">
          <label>Type:</label>
          <select name="type" value={formData.type} onChange={handleChange} className="form-control">
            <option value="">Select Type</option>
            <option value="Jeans">Jeans</option>
            <option value="Sneakers">Sneakers</option>
            <option value="T-Shirts">T-Shirts</option>
            <option value="Boots">Boots</option>
            <option value="Shoes">Shoes</option>
            <option value="Dresses">Dresses</option>
            <option value="Skirts">Skirts</option>
            <option value="Shirts">Shirts</option>
            <option value="Jackets">Jackets</option>
          </select>
        </div>
        <div className="form-group">
          <label>Price: (USD)</label>
          <input type="number" name="price" value={formData.price} onChange={handleChange} required className="form-control" />
        </div>
        <div className="form-group">
          <label>Image:</label>
          <input type="file" accept="image/*" onChange={handleImageChange} className="form-control-file" />
        </div>
        <button type="submit" className="btn btn-primary">Create Advertisement</button>
      </form>
    </div>
  );
}
