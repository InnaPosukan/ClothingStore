using ClothingStoreApi.Models;
using ClothingStoreApi.DTO;
using ClothingStoreApi.DBContext;
using ClothingStoreApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStoreApi.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly ClothingStoreContext _dbContext;

        public DiscountService(ClothingStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> CalculatePriceAfterDiscount(DiscountCalculationDTO dto)
        {
            try
            {
                Advertisement? advertisement = await _dbContext.Advertisements
                    .FirstOrDefaultAsync(a => a.AdId == dto.AdvertisementId);

                if (advertisement != null && advertisement.Price.HasValue)
                {
                    decimal originalPrice = advertisement.Price.Value;

                    decimal discountDecimal = dto.DiscountPercent / 100;

                    decimal discountedPrice = originalPrice * (1 - discountDecimal);

                    if (discountedPrice >= 0)
                    {
                        var discount = new Discount
                        {
                            AdId = dto.AdvertisementId,
                            DiscountPercentage = dto.DiscountPercent
                        };
                        _dbContext.Discounts.Add(discount);
                        await _dbContext.SaveChangesAsync();
                        return discountedPrice;
                    }
                    else
                    {
                        throw new InvalidOperationException("Resulting price after discount is negative");
                    }
                }
                else
                {
                    throw new InvalidOperationException("Advertisement not found or price is not set");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while calculating price after discount", ex);
            }
        }

        public async Task<bool> DeleteDiscount(int advertisementId)
        {
            try
            {
                var discount = await _dbContext.Discounts.FirstOrDefaultAsync(d => d.AdId == advertisementId);
                if (discount != null)
                {
                    _dbContext.Discounts.Remove(discount);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error occurred while deleting discount", ex);
            }
        }
    }
}
