using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using ClothingStoreApi.Services;
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
      
        [HttpGet("ad/{adId}")]
        public async Task<ActionResult<List<Rating>>> GetRatingsForAdAsync(int adId)
        {
            try
            {
                var ratings = await _ratingService.GetRatingsForAdAsync(adId);
                if (ratings == null || ratings.Count == 0)
                {
                    var advertisement = await _ratingService.GetRatingsForAdAsync(adId);
                    if (advertisement == null)
                    {
                        return NotFound($"Advertisement with ID {adId} not found");
                    }
                    return Ok(advertisement);
                }
                return Ok(ratings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{adId}")]
        public async Task<ActionResult<double>> GetAverageRating(int adId)
        {
            try
            {
                var averageRating = await _ratingService.CalculateAverageRating(adId);
                return Ok(averageRating);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}