using ClothingStoreApi.DTO;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using ClothingStoreApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClothingStoreApi.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("addOrder")]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var orders = await _orderService.CreateOrder(order);
            return Ok(orders);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<Order>>> GetUserOrders(int userId)
        {
            var orders = await _orderService.GetUserOrders(userId);
            return Ok(orders);
        }

        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            try
            {
                var deletedOrder = await _orderService.DeleteOrder(orderId);
                return Ok(deletedOrder);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> UpdateOrderStatus([FromBody] OrderStatusUpdateDTO orderStatusUpdateDTO)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateOrderStatus(orderStatusUpdateDTO);
                return Ok(updatedOrder);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}