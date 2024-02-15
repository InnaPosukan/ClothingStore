using ClothingStoreApi.DBContext;
using ClothingStoreApi.DTO;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreApi.Services
{
    public class AdvertisementService : IAdvertisementService
    {
        private readonly ClothingStoreContext _dbContext;
        private readonly IConfiguration _configuration;

        public AdvertisementService(ClothingStoreContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<AdvertisementDTO> CreateAdvertisement(AdvertisementDTO advertisementDTO)
        {
            if (advertisementDTO == null)
            {
                throw new ArgumentNullException(nameof(advertisementDTO));
            }

            var advertisement = new Advertisement
            {
                Title = advertisementDTO.Title,
                Description = advertisementDTO.Description,
                AdvImage = advertisementDTO.AdvImage,
                Price = advertisementDTO.Price,
                PublicationDate = DateTime.Now,
            };

            var advertisementAttribute = new AdvertisementAttribute
            {
                Size = advertisementDTO.Size,
                Color = advertisementDTO.Color,
                Brand = advertisementDTO.Brand,
                Type = advertisementDTO.Type,
            };

            advertisement.AdvertisementAttributes.Add(advertisementAttribute);

            _dbContext.Advertisements.Add(advertisement);
            await _dbContext.SaveChangesAsync();

            return advertisementDTO;
        }

        public async Task<Advertisement> GetAdvertisementById(int id)
        {
            try
            {
                var advertisement = await _dbContext.Advertisements
                    .Include(a => a.AdvertisementAttributes)
                    .FirstOrDefaultAsync(a => a.AdId == id);

                if (advertisement == null)
                {
                    throw new Exception("Advertisement not found");
                }

                return advertisement;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving advertisement: {ex}");
                throw;
            }
        }

        public async Task<List<Advertisement>> GetAllAdvertisements()
        {
            try
            {
                var advertisements = await _dbContext.Advertisements
                    .Include(a => a.AdvertisementAttributes)
                    .ToListAsync();

                return advertisements;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving advertisements: {ex}");
                throw;
            }
        }

        public async Task<bool> DeleteAdvertisement(int id)
        {
            try
            {
                var advertisement = await _dbContext.Advertisements.FirstOrDefaultAsync(a => a.AdId == id);
                if (advertisement == null)
                {
                    throw new Exception("Advertisement not found");
                }

                _dbContext.Advertisements.Remove(advertisement);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting advertisement: {ex}");
                throw;
            }
        }
    }
}
