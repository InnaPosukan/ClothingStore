using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ClothingStoreApi.Models;
using ClothingStoreApi.Services;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.DTO;

namespace ClothingStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromBody] User user)
        {
            try
            {
                var token = await _userService.Login(user);
                return Ok(new
                {
                    token,
                    expiration = DateTime.Now.AddHours(24)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("registration")]
        public async Task<IActionResult> UserRegistration([FromBody] User user)
        {
            try
            {
                var result = await _userService.Register(user);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("updateInfo")]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserDTO updateUser)
        {
            try
            {
                var result = await _userService.UpdateUserInfo(updateUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("getUserbyId/{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);  
            if(user == null)
            {
                return NotFound();
            }
            return user;
        }
    }
}
