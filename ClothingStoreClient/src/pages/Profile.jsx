import React, { useEffect, useState } from 'react';
import { useAuth } from '../context/AuthContext';
import { getUserById, updateUserInfo } from '../http/AuthApi';
import '../styles/Profile.css';
import avatar from '../assets/avatar.png';
import { Link } from 'react-router-dom';

export default function Profile() {
  const { userId } = useAuth();
  const [userData, setUserData] = useState(null);
  const [editedUserData, setEditedUserData] = useState({});

  const fetchUserData = async () => {
    try {
      const userData = await getUserById(userId);
      setUserData(userData);
    } catch (error) {
      console.error('Failed to fetch user:', error);
    }
  };

  useEffect(() => {
    if (userId) {
      fetchUserData();
    }
  }, [userId]); 

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditedUserData({
      ...editedUserData,
      [name]: value
    });
    console.log(`Updated ${name}: ${value}`);
  };
  
  const handleSave = async () => {
    try {
      const dataToUpdate = { ...editedUserData, userId };
      console.log('Data to update:', dataToUpdate);
      await updateUserInfo(dataToUpdate);
      await fetchUserData();
    } catch (error) {
      console.error('Failed to update user info:', error);
    }
  };
  
  return (
    <div className="profile-container">
      <div className='avatar-container'>
        <div className='title-user'> Site user</div>
        <img className='avatar-image' src={avatar} alt="Avatar" />
        <p className='info-email'>{editedUserData.email || (userData && userData.email)}</p>
        <Link to="/history" className='history-btn'>History of orders</Link>
        <Link to="/my" className='my-orders-btn'>My orders</Link>

      </div>
      <div className='info-container'>
        <div className='info-profile'>Profile information</div>
        {userData && (
          <div className="input-container">
            <div className="input-item">
              <div className="input-label">First Name:</div>
              <input 
                type="text" 
                name="firstName" 
                value={(editedUserData.firstName !== undefined && editedUserData.firstName !== '') ? editedUserData.firstName : (userData && userData.firstName) || 'undefined'} 
                onChange={handleInputChange} 
              />
            </div>
            <div className="input-item">
              <div className="input-label">Last Name:</div>
              <input 
                type="text" 
                name="lastName" 
                value={(editedUserData.lastName !== undefined && editedUserData.lastName !== '') ? editedUserData.lastName : (userData && userData.lastName) || 'undefined'} 
                onChange={handleInputChange} 
              />
            </div>
            <div className="input-item">
              <div className="input-label">Phone Number:</div>
              <input 
                type="text" 
                name="phoneNumber" 
                value={(editedUserData.phoneNumber !== undefined && editedUserData.phoneNumber !== '') ? editedUserData.phoneNumber : (userData && userData.phoneNumber) || 'undefined'} 
                onChange={handleInputChange} 
              />
            </div>
            <div className="input-item">
              <div className="input-label">Sex:</div>
              <select 
                className="select-dropdown" 
                name="sex" 
                value={(editedUserData.sex !== undefined && editedUserData.sex !== '') ? editedUserData.sex : (userData && userData.sex) || 'undefined'} 
                onChange={handleInputChange}
              >
                <option value="male">Male</option>
                <option value="female">Female</option>
              </select>
            </div>
            <div className="input-item">
              <div className="input-label">Email:</div>
              <input 
                type="text" 
                name="email" 
                value={(editedUserData.email !== undefined && editedUserData.email !== '') ? editedUserData.email : (userData && userData.email) || 'undefined'} 
                onChange={handleInputChange} 
              />
            </div>
            <div className="input-item">
              <div className="input-label">Address:</div>
              <input 
                type="text" 
                name="address" 
                value={(editedUserData.address !== undefined && editedUserData.address !== '') ? editedUserData.address : (userData && userData.address) || 'undefined'} 
                onChange={handleInputChange} 
              />
            </div>
          </div>
        )}
        <button className='save-btn' onClick={handleSave}>Save</button>
      </div>
    </div>
  );
}
