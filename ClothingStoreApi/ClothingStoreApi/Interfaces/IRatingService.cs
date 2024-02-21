using ClothingStoreApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreApi.Interfaces
{
    public interface IRatingService
    {
        Task<Rating> AddRatingToAdAsync(Rating rating);
        Task<double> CalculateAverageRating(int adId);

        Task<List<Rating>> GetRatingsForAdAsync(int adId);


    }
}
