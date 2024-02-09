using ClothingStoreApi.Models;

namespace ClothingStoreApi.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(User user);
        Task<string> Register(User user);
    }

}
