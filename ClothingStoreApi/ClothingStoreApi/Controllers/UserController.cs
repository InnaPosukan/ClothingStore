using ClothingStoreApi.DBContext;
using ClothingStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ClothingStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ClothingStoreContext _dbcontext;
        public UserController(ClothingStoreContext dbcontext)
        {
            _dbcontext = dbcontext; 
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                List<User> listUsers = _dbcontext.Users.ToList();
                if(listUsers != null)
                {
                    return Ok(listUsers);
                }
                return Ok("There are not users in the database");
                
            }catch(Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

    }
}
