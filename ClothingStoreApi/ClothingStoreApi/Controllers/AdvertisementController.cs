using ClothingStoreApi.DTO;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpPost("createAdvertisement")]
        public async Task<IActionResult> CreateAdvertisement([FromForm] AdvertisementDTO advertisementDTO, IFormFile image)
        {
            try
            {
                var createdAdvertisement = await _advertisementService.CreateAdvertisement(advertisementDTO, image);
                return Ok(createdAdvertisement);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the advertisement.");
            }
        }
    

    [HttpGet("getAdvertisementById/{id}")]
        public async Task<IActionResult> GetAdvertisementById(int id)
        {
            try
            {
                var advertisement = await _advertisementService.GetAdvertisementById(id);
                return Ok(advertisement);
            }
            catch (Exception ex)
            {
                return NotFound($"Advertisement with ID {id} not found: {ex.Message}");
            }
        }

        [HttpGet("getAllAdvertisements")]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAllAdvertisements()
        {
            try
            {
                var advertisements = await _advertisementService.GetAllAdvertisements();
                return Ok(advertisements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("deleteAdvertisement/{id}")]
        public async Task<IActionResult> DeleteAdvertisement(int id)
        {
            try
            {
                var result = await _advertisementService.DeleteAdvertisement(id);
                if (result)
                {
                    return Ok($"Advertisement with ID {id} has been deleted successfully");
                }
                else
                {
                    return NotFound($"Advertisement with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("with-discounts")]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAdvertisementsWithDiscounts()
        {
            try
            {
                var advertisements = await _advertisementService.GetAdvertisementsWithDiscounts();
                return Ok(advertisements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving advertisements with discounts: {ex.Message}");
            }
        }
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Advertisement>>> GetAllAdvertisementsByUser(int userId)
        {
            var advertisements = await _advertisementService.GetAllAdvertisementsByUser(userId);
            return advertisements;
        }

    }
}
