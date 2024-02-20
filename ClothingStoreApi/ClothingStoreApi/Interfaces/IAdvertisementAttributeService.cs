using ClothingStoreApi.Models;

namespace ClothingStoreApi.Interfaces
{
    public interface IAdvertisementAttributeService
    {
        IQueryable<Advertisement> FilterAdvertisementsByCategory(string category);

    }
}
