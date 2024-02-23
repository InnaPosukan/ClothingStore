import React, { useState, useEffect, useContext } from 'react';
import { getAdById } from '../http/OrderApi';
import { useAuth } from '../context/AuthContext';
import { BASE_URL } from '../utils/apiConfig';
import '../styles/OrderHistory.css';

export default function OrderHistory() {
    const { userId } = useAuth();
    const [orders, setOrders] = useState([]);

    useEffect(() => {
        async function fetchOrders() {
            try {
                const ordersData = await getAdById(userId); 
                console.log('Orders data:', ordersData);

                setOrders(ordersData);
            } catch (error) {
                console.error('Error fetching orders:', error.message);
            }
        }

        fetchOrders();
    }, [userId]); 

    return (
        <div>
            <h2 className='history-title'>History of orders</h2>
            {orders.map(order => (
                <div key={order.ad.adId} className="container">
                    {order.ad.imagePath && (
                        <img src={`${BASE_URL}/images/${order.ad.imagePath}`} className="advertisement-img" alt="Advertisement" />
                    )}
                    <div>
                        <p>Order number: {order.orderId}</p>
                        <p>Order status: {order.status}</p>
                        <p>Title: {order.ad.title}</p>
                        <p>Quantity: {order.quantity}</p>
                        <p>Description: {order.ad.description}</p>
                        <p>Price: {order.ad.price}$</p>
                    </div>
                </div>
            ))}
        </div>
    );
}
