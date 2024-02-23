using ClothingStoreApi.DBContext;
using ClothingStoreApi.DTO;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

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
        public async Task<AdvertisementDTO> CreateAdvertisement(AdvertisementDTO advertisementDTO, IFormFile image)
        {
            try
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new ArgumentException("Invalid image file format.", nameof(image));
                }

                if (advertisementDTO.Price <= 0)
                {
                    throw new ArgumentException("Price must be greater than zero.", nameof(advertisementDTO.Price));
                }

                var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                if (!Directory.Exists(wwwrootPath))
                {
                    Directory.CreateDirectory(wwwrootPath);
                }

                var imagesPath = Path.Combine(wwwrootPath, "images");

                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(imagesPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                var advertisement = new Advertisement
                {
                    Title = advertisementDTO.Title,
                    Description = advertisementDTO.Description,
                    Price = advertisementDTO.Price,
                    PublicationDate = DateTime.Now,
                    ImagePath = fileName,
                    SellerId = advertisementDTO.SellerId
                };

                var advertisementAttribute = new AdvertisementAttribute
                {
                    Size = advertisementDTO.Size,
                    Color = advertisementDTO.Color,
                    Brand = advertisementDTO.Brand,
                    Type = advertisementDTO.Type,
                    Category = advertisementDTO.Category,
                };

                advertisement.AdvertisementAttributes.Add(advertisementAttribute);

                _dbContext.Advertisements.Add(advertisement);
                await _dbContext.SaveChangesAsync();

                return advertisementDTO;
            }
            catch (Exception ex)
            {
                throw;
            }
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

                var relatedAttributes = await _dbContext.AdvertisementAttributes.Where(aa => aa.AdId == id).ToListAsync();
                _dbContext.AdvertisementAttributes.RemoveRange(relatedAttributes);

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
        public async Task<List<Advertisement>> GetAllAdvertisementsByUser(int userId)
        {
            try
            {
                var advertisements = await _dbContext.Advertisements
                    .Where(a => a.SellerId == userId)
                    .Include(a => a.AdvertisementAttributes)
                    .ToListAsync();

                return advertisements;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving advertisements for user {userId}: {ex}");
                throw;
            }
        }
    }
}
