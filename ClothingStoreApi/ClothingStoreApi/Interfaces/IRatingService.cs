using ClothingStoreApi.Models;

namespace ClothingStoreApi.Interfaces
{
    public interface IRatingService
    {
        Task<Rating> AddRatingToAdAsync(Rating rating);
        Task<double> CalculateAverageRating(int adId);



    }
}
