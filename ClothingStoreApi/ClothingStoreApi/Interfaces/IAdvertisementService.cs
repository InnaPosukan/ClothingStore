using ClothingStoreApi.DTO;
using ClothingStoreApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreApi.Interfaces
{
    public interface IAdvertisementService
    {
        Task<AdvertisementDTO> CreateAdvertisement(AdvertisementDTO advertisementDTO);
        Task<Advertisement> GetAdvertisementById(int id);
        Task<List<Advertisement>> GetAllAdvertisements();
        Task<bool> DeleteAdvertisement(int id);
        Task<List<AdvertisementDTO>> GetAdvertisementsWithDiscounts();
    }
}
