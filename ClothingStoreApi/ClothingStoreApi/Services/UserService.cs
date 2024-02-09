using ClothingStoreApi.DBContext;
using ClothingStoreApi.Helpers;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public async Task<string> Register(User user)
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

            return "User is successfully registered";
        }
    }
}
