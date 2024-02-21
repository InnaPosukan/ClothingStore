import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { getAdvertisementById } from '../http/AdvetisementApi';
import { addRatingToAdvertisement, getRatingsForAdvertisement, getAverageRatingForAdvertisement } from '../http/RatingApi';
import '../styles/AdvertisementPage.css';
import { BASE_URL } from '../utils/apiConfig';
import { useAuth } from '../context/AuthContext';

export default function AdvertisementPage() {
  const { userId } = useAuth();
  const { id } = useParams();
  const [advertisement, setAdvertisement] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [review, setReview] = useState('');
  const [rating, setRating] = useState(0);
  const [ratings, setRatings] = useState([]);
  const [averageRating, setAverageRating] = useState(null); // Добавляем состояние для среднего рейтинга

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [advertisementData, ratingsData] = await Promise.all([
          getAdvertisementById(id),
          getRatingsForAdvertisement(id)
        ]);
        setAdvertisement(advertisementData);
        setRatings(ratingsData);

        // После получения информации о рекламе и оценках, вызываем функцию для получения среднего рейтинга
        const averageRatingData = await getAverageRatingForAdvertisement(id);
        setAverageRating(averageRatingData);

        setLoading(false);
      } catch (error) {
        setError(error.message);
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  const handleReviewSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await addRatingToAdvertisement({
        adId: advertisement.adId,
        userId,
        ratingValue: rating,
        review,
        ratingDate: new Date().toISOString()
      });
      console.log('Rating added successfully:', response);
      console.log('Review posted successfully');

      // Обновляем отзывы после добавления нового отзыва
      const updatedRatings = await getRatingsForAdvertisement(id);
      setRatings(updatedRatings);
    } catch (error) {
      console.error('Error adding rating:', error.message);
    }
  };

  return (
    <div className="advertisement-page">
      <div className="advertisement-cont">
        {loading ? (
          <div className="loading">Loading...</div>
        ) : error ? (
          <div className="error">Error: {error}</div>
        ) : !advertisement ? (
          <div className="not-found">Advertisement not found</div>
        ) : (
          <>
            <div className="img-cont">
              <img src={`${BASE_URL}/images/${advertisement.imagePath}`} className="advertisement-img" alt="Advertisement" />
            </div>
            <div className="adv-details">
              <div className="advertisement-info">
                <p className="advertisement-title">{advertisement.title}</p>
                <p className="advertisement-id">Артикул: {advertisement.adId}</p>
              </div>
              {averageRating !== null && (
                <p className="advertisement-average-rating">
                  Rating {averageRating}★ ({ratings.length})
                </p>
              )}
              <p className="advertisement-price">{advertisement.price} $</p>
              <p className="advertisement-description">Description: {advertisement.description}</p>
              <div className="advertisement-attributes">
                {advertisement.advertisementAttributes.map((attribute) => (
                 <span key={attribute.id} className="advertisement-attribute">
                 <span className="attribute-circle">
                   Brand: {attribute.brand}
                 </span>
                 <span className="attribute-circle">
                   Size: {attribute.size}
                 </span>
                 <span className="attribute-circle">
                   Color: {attribute.color}
                 </span>
                 <span className="attribute-circle">
                   Type: {attribute.type}
                 </span>
               </span>
               
                ))}
              </div>

            </div>
          </>
        )}
      </div>
      <div className="review-container">
        <form className="review-form" onSubmit={handleReviewSubmit}>
          <textarea
            className="review-textarea"
            value={review}
            onChange={(e) => setReview(e.target.value)}
            placeholder="Enter your review"
            rows={4}
          />
          <input
            className="rating-input"
            type="number"
            value={rating}
            onChange={(e) => setRating(parseInt(e.target.value))}
            min={0}
            max={5}
            step={0.1}
            placeholder="Enter rating (0-5)"
          />
          <button className="submit-button" type="submit">Submit Review</button>
        </form>
        {ratings.length > 0 && (
          <div className="advertisement-ratings">
            <ul className="ratings-list">
              {ratings.map((rating) => (
                <li key={rating.ratingId} className="rating-item">
                  <div className="rating-value-container">
                    <p className="rating-value">Rating Value: {rating.ratingValue}</p>
                  </div>
                  <p>Review: {rating.review}</p>
                  <p>Rating Date: {new Date(rating.ratingDate).toLocaleString()}</p>
                  <p>User Email: {rating.user.email}</p>
                </li>
              ))}
            </ul>
          </div>
        )}
      </div>
    </div>
  );
}
