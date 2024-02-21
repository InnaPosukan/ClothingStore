using ClothingStoreApi.DBContext;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace ClothingStoreApi.Services
{
    public class AdvertisementAttributeService : IAdvertisementAttributeService
    {
        private readonly ClothingStoreContext _dbContext;
        private readonly IConfiguration _configuration;

        public AdvertisementAttributeService(ClothingStoreContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public IQueryable<Advertisement> FilterAdvertisementsByCategory(string category)
        {
            IQueryable<Advertisement> filteredAdvertisements = _dbContext.Advertisements
                .Include(ad => ad.AdvertisementAttributes) 
                .Where(ad => ad.AdvertisementAttributes.Any(attr => attr.Category == category));

            return filteredAdvertisements;
        }

    }
}