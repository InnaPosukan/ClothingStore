using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClothingStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService _ratingService;

        public RatingController(IRatingService ratingService)
        {
            _ratingService = ratingService; 
        }

        [HttpPost("addRating")]
        public async Task<IActionResult> AddRating([FromBody] Rating rating)
        {
            try
            {
                var addedRating = await _ratingService.AddRatingToAdAsync(rating);
                return Ok(addedRating);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка при добавлении рейтинга.");
            }
        }
        [HttpGet("average/{adId}")]
        public async Task<IActionResult> GetAverageRatingForAd(int adId)
        {
            try
            {
                var averageRating = await _ratingService.CalculateAverageRating(adId);
                return Ok(averageRating);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Произошла ошибка при вычислении среднего рейтинга.");
            }
        }
    }
}