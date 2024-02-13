using ClothingStoreApi.DTO;
using ClothingStoreApi.Models;

namespace ClothingStoreApi.Interfaces
{
    public interface IUserService
    {
        Task<string> Login(User user);
        Task<User> Register(User user);
        Task<User> UpdateUserInfo(UpdateUserDTO user);
        public Task<User?> GetUserById(int Id);
    }
}
