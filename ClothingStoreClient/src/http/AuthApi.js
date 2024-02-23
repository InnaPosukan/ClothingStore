import axios from 'axios';
import { BASE_URL } from '../utils/apiConfig';

export const authenticate = async (isLoginMode, email, password, phoneNumber, firstName, lastName) => {
  const url = `${BASE_URL}/api/User/${isLoginMode ? 'login' : 'registration'}`;
  const requestBody = isLoginMode
    ? { email, password }
    : { email, password, phoneNumber, firstName, lastName };

  try {
    const response = await axios.post(url, requestBody, {
      headers: {
        'Content-Type': 'application/json',
      },
    });

    return response.data; 
  } catch (error) {
    throw error;
  }
};
export const getUserById = async (id) => {
  try {
    const response = await axios.get(`${BASE_URL}/api/User/getUserbyId/${id}`);
    return response.data;
  } catch (error) {
    console.error('Failed to fetch user:', error);
    throw error;
  }
};
export const updateUserInfo = async (userData) => {
  try {
    const response = await axios.put(`${BASE_URL}/api/User/updateInfo`, userData, {
      headers: {
        'Content-Type': 'application/json',
      },
    });

    return response.data; 
  } catch (error) {
    throw error;
  }
};