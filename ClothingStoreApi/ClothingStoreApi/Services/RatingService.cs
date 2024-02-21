using ClothingStoreApi.DBContext;
using ClothingStoreApi.DTO;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreApi.Services
{
    public class RatingService : IRatingService
    {
        private readonly ClothingStoreContext _dbContext;

        public RatingService(ClothingStoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Rating> AddRatingToAdAsync(Rating rating)
        {
            var ad = await _dbContext.Advertisements.FirstOrDefaultAsync(a => a.AdId == rating.AdId);
            if (ad == null)
            {
                throw new ArgumentException($"Объявление с ID {rating.AdId} не найдено.");
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == rating.UserId);
            if (user == null)
            {
                throw new ArgumentException($"Пользователь с ID {rating.UserId} не найден.");
            }

            rating.RatingDate = DateTime.Now;

            _dbContext.Ratings.Add(rating);
            await _dbContext.SaveChangesAsync();

            return rating;
        }
        public async Task<double> CalculateAverageRating(int adId)
        {
            var ratings = await _dbContext.Ratings.Where(r => r.AdId == adId).ToListAsync();
            if (ratings.Count == 0)
            {
                return 0; 
            }

            double sum = ratings.Sum(r => (double)r.RatingValue.GetValueOrDefault());
            double average = sum / ratings.Count;
            average = Math.Round(average, 1);

            return average;
        }
        public async Task<List<Rating>> GetRatingsForAdAsync(int adId)
        {
            return await _dbContext.Ratings
                .Include(r => r.User) 
                .Where(r => r.AdId == adId)
                .ToListAsync();
        }



    }
}