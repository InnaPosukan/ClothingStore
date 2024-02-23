import axios from 'axios';
import { BASE_URL } from '../utils/apiConfig';

export const addOrder = async (orderData) => {
    try {
        const response = await axios.post(`${BASE_URL}/api/orders/addOrder`, orderData);
        return response.data;
    } catch (error) {
        throw new Error('Error creating order: ' + error.message);
    }
};
export const getAdById = async (adId) => {
    try {
        const response = await axios.get(`${BASE_URL}/api/orders/${adId}`);
        return response.data;
    } catch (error) {
        throw new Error('Ошибка при получении объявления: ' + error.message);
    }
};
