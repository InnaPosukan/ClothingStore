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