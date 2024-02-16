using Microsoft.AspNetCore.Mvc;
using ClothingStoreApi.DTO;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Services;

namespace ClothingStoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpPost("calculate")]
        public async Task<IActionResult> CalculatePriceAfterDiscount([FromBody] DiscountCalculationDTO dto)
        {
            try
            {
                decimal discountedPrice = await _discountService.CalculatePriceAfterDiscount(dto);
                return Ok(discountedPrice);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("delete/{advertisementId}")]
        public async Task<IActionResult> DeleteDiscount(int advertisementId)
        {
            try
            {
                bool deleted = await _discountService.DeleteDiscount(advertisementId);
                if (deleted)
                {
                    return Ok("Discount deleted successfully.");
                }
                else
                {
                    return NotFound("Discount not found for the given advertisement ID.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
   