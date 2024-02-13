using ClothingStoreApi.DBContext;
using ClothingStoreApi.DTO;
using ClothingStoreApi.Helpers;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace ClothingStoreApi.Services
{
    public class UserService : IUserService
    {
        private readonly ClothingStoreContext _dbContext;
        private readonly IConfiguration _configuration;

        public UserService(ClothingStoreContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<string> Login(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Invalid user or password");
            }

            string hashedPassword = HashPasswordHelper.hashPassword(user.Password);

            var dbUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == hashedPassword);

            if (dbUser == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, dbUser.Email),
                new Claim("UserId", dbUser.UserId.ToString())
            };

            var token = TokenHelper.GenerateToken(_configuration, authClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<User> Register(User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
            {
                throw new ArgumentException("Invalid user information");
            }

            var existingUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Username already exists");
            }

            user.Password = HashPasswordHelper.hashPassword(user.Password);
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
        public async Task<User> UpdateUserInfo(UpdateUserDTO updateUser)
        {
            if (updateUser == null || updateUser.UserId <= 0)
            {
                throw new ArgumentException("Invalid user information");
            }

            var existingUser = await _dbContext.Users.FindAsync(updateUser.UserId);

            if (existingUser == null)
            {
                throw new InvalidOperationException("User not found");
            }
            existingUser.Email = updateUser.Email ?? existingUser.Email;
            existingUser.FirstName = updateUser.FirstName ?? existingUser.FirstName;
            existingUser.LastName = updateUser.LastName ?? existingUser.LastName;
            existingUser.PhoneNumber = updateUser.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.Avatar = updateUser.Avatar ?? existingUser.Avatar;
            existingUser.Sex = updateUser.Sex ?? existingUser.Sex;
            existingUser.DateOfBirth = updateUser.DateOfBirth ?? existingUser.DateOfBirth;
            existingUser.Address = updateUser.Address ?? existingUser.Address;

            await _dbContext.SaveChangesAsync();

            return existingUser;
        }
        public async Task<User?> GetUserById(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == id);
            return user;
        }
    }
}