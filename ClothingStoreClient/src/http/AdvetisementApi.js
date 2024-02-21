import axios from 'axios';
import { BASE_URL } from '../utils/apiConfig';

export const createAdvertisement = async (formData) => {
    try {
        const response = await axios.post(`${BASE_URL}/api/Advertisement/createAdvertisement`, formData, {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        });
        return response.data;
    } catch (error) {
        throw new Error('Error creating advertisement: ' + error.message);
    }
};

export const getAllAdvertisements = async () => {
    try {
        const response = await axios.get(`${BASE_URL}/api/Advertisement/getAllAdvertisements`);
        
        return response.data;
    } catch (error) {
        throw new Error('Error fetching advertisements: ' + error.message);
    }
};
export const FilterAdvertisementsByCategory = async (category) => {
  try {
      const response = await axios.get(`${BASE_URL}/api/AdvertisementAttribute/${category}`);
      
      return response.data;
  } catch (error) {
      throw new Error('Error filtering advertisements by category: ' + error.message);
  }
};
export const getAdvertisementById = async (id) => {
    try {
        const response = await axios.get(`${BASE_URL}/api/Advertisement/getAdvertisementById/${id}`);
        
        return response.data;
    } catch (error) {
        throw new Error('Error fetching advertisement by ID: ' + error.message);
    }
};
