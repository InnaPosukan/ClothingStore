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

            if (advertisementDTO.AdvImage != null)
            {
                advertisement.AdvImage = advertisementDTO.AdvImage;
            }

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
        public async Task<List<AdvertisementDTO>> GetAdvertisementsWithDiscounts()
        {
            try
            {
                var advertisements = await _dbContext.Advertisements
                    .Include(a => a.AdvertisementAttributes)
                    .Where(a => _dbContext.Discounts.Any(d => d.AdId == a.AdId))
                    .ToListAsync();

                var advertisementDTOs = advertisements.Select(a => new AdvertisementDTO
                {
                    AdId = a.AdId,
                    Title = a.Title,
                    Description = a.Description,
                    AdvImage = a.AdvImage,
                    Price = a.Price,
                    DiscountPercent = _dbContext.Discounts.FirstOrDefault(d => d.AdId == a.AdId)?.DiscountPercentage,
                    Size = a.AdvertisementAttributes.FirstOrDefault()?.Size,
                    Color = a.AdvertisementAttributes.FirstOrDefault()?.Color,
                    Brand = a.AdvertisementAttributes.FirstOrDefault()?.Brand
                }).ToList();

                return advertisementDTOs;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving advertisements with discounts: {ex}");
                throw;
            }
        }


    }
}

