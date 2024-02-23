import React, { useEffect, useState } from 'react';
import { useCart } from '../context/CartContex';
import { getUserById } from '../http/AuthApi';
import { addOrder } from '../http/OrderApi';
import '../styles/OrderPage.css';
import { BASE_URL } from '../utils/apiConfig';
import { useAuth } from '../context/AuthContext';

function OrderPage() {
    const { cartItems, clearCart } = useCart(); 
    const [userInfo, setUserInfo] = useState(null);
    const { userId } = useAuth(); 
    const [totalPrice, setTotalPrice] = useState(0);
    console.log("cartItems:", cartItems);
    console.log("userId:", userId); 

    useEffect(() => {
        const fetchUserInfo = async () => {
            try {
                const userData = await getUserById(userId);
                setUserInfo(userData);
            } catch (error) {
                console.error('Failed to fetch user:', error);
            }
        };

        fetchUserInfo();
    }, [userId]);

    useEffect(() => {
        let total = 0;
        cartItems.forEach(item => {
            total += item.totalPrice;
        });
        setTotalPrice(total);
    }, [cartItems]);

    const handleOrder = async (item) => {
        try {
            if (!item.adId || !item.quantity) {
                console.error('Invalid item data:', item);
                return;
            }
    
            const orderData = {
                userId: userId,
                status: "In process",
                adId: item.adId,
                quantity: item.quantity
            };
            console.log("Data sent to server:", orderData);
            await addOrder(orderData);
            alert('Order created successfully!');
            clearCart();
        } catch (error) {
            console.error('Error creating order:', error);
            alert('Failed to create order');
        }
    };
    
    
    return (
        <div className="order-page">
            <h1 className="order-title">Оформить заказ</h1>
            <div className='detail-container'>
                <div className='user-info'>
                    <h3 className="user-title">Личные данные</h3>
                    {userInfo && (
                        <ul>
                            <li>
                                <label>Email:</label>
                                <input type="text" value={userInfo.email} readOnly />
                            </li>
                            <li>
                                <label>Имя:</label>
                                <input type="text" value={userInfo.firstName} readOnly />
                            </li>
                            <li>
                                <label>Фамилия:</label>
                                <input type="text" value={userInfo.lastName} readOnly />
                            </li>
                            <li>
                                <label>Телефон:</label>
                                <input type="text" value={userInfo.phoneNumber} readOnly />
                            </li>
                        </ul>
                    )}
                    <h3 className="delivery-title">Доставка</h3>
                    <ul>
                        <li>
                            <label>Страна:</label>
                            <input type="text" value="Ukraine" readOnly />
                        </li>
                        <li>
                            <label>Adress:</label>
                            <input type="text" value={"Adress"} readOnly />
                        </li>
                    </ul>
                </div>
                <div className='detail-info'>
                    <h3 className="user-title">Ваш заказ</h3>
                    <ul>
                        {cartItems.map((item, index) => (
                            <div key={index} className="cart-item-container">
                                <div className="im-container">
                                    <img src={`${BASE_URL}/images/${item.imagePath}`} className="advertisement-img" alt="Advertisement" />
                                </div>
                                <div className="item-details">
                                    <p className="cart-title">{item.title}</p>
                                    <p>Price: ${item.totalPrice}</p>
                                    <p>Id: ${item.adId}</p>
                                    <p>Quantity: {item.quantity}</p>
                                    <button className="order-button" onClick={() => { console.log(item); handleOrder(item); }}>Create Order</button>
                                </div>
                            </div>
                        ))}
                    </ul>
                    <div className="total-price">Total Price: ${totalPrice}</div>
                </div>
            </div>
        </div>
    );
}

export default OrderPage;
