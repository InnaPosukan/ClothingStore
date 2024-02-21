import { BASE_URL } from "../utils/apiConfig";
import axios from "axios";
export const addRatingToAdvertisement = async (ratingData) => {
    try {
        const response = await axios.post(`${BASE_URL}/api/Rating/addRating`, ratingData);
        return response.data;
    } catch (error) {
        throw new Error('Error adding rating to advertisement: ' + error.message);
    }
};

export const getRatingsForAdvertisement = async (adId) => {
    try {
        const response = await axios.get(`${BASE_URL}/api/Rating/ad/${adId}`);
        return response.data;
    } catch (error) {
        throw new Error('Error fetching ratings for advertisement: ' + error.message);
    }
};
export const getAverageRatingForAdvertisement = async (adId) => {
    try {
        const response = await axios.get(`${BASE_URL}/api/Rating/${adId}`);
        return response.data;
    } catch (error) {
        throw new Error('Error fetching average rating for advertisement: ' + error.message);
    }
};