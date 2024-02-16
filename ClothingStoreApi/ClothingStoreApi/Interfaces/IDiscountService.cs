using ClothingStoreApi.DTO;

namespace ClothingStoreApi.Interfaces
{
    public interface IDiscountService
    {

        Task<decimal> CalculatePriceAfterDiscount(DiscountCalculationDTO dto);
        Task<bool> DeleteDiscount(int advertisementId);


    }


}
