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

                // Получаем путь к wwwroot
                var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                // Проверяем существует ли папка wwwroot, если нет, то создаем ее
                if (!Directory.Exists(wwwrootPath))
                {
                    Directory.CreateDirectory(wwwrootPath);
                }

                // Получаем путь к папке images внутри wwwroot
                var imagesPath = Path.Combine(wwwrootPath, "images");

                // Проверяем существует ли папка images, если нет, то создаем ее
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                // Генерируем уникальное имя файла
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var filePath = Path.Combine(imagesPath, fileName);

                // Сохраняем изображение в файловой системе
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Создаем объект объявления
                var advertisement = new Advertisement
                {
                    Title = advertisementDTO.Title,
                    Description = advertisementDTO.Description,
                    Price = advertisementDTO.Price,
                    PublicationDate = DateTime.Now,
                    ImagePath = fileName
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

                // Добавляем объявление в контекст данных и сохраняем изменения в базе данных
                _dbContext.Advertisements.Add(advertisement);
                await _dbContext.SaveChangesAsync();

                // Возвращаем созданное объявление
                return advertisementDTO;
            }
            catch (Exception ex)
            {
                // Логируем ошибку и выбрасываем исключение дальше
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

