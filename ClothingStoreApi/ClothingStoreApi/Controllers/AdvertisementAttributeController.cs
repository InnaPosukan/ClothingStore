using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementAttributeController : ControllerBase
    {
        private readonly IAdvertisementAttributeService _advertisementAttributeService;

        public AdvertisementAttributeController(IAdvertisementAttributeService advertisementAttributeService)
        {
            _advertisementAttributeService = advertisementAttributeService;
        }

        [HttpGet("{category}")]
        public IActionResult GetByCategory(string category)
        {
            var advertisements = _advertisementAttributeService.FilterAdvertisementsByCategory(category);

            if (advertisements == null)
            {
                return NotFound();
            }

            return Ok(advertisements);
        }
    }
}
