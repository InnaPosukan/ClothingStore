using System.Collections.Generic;
using System.Threading.Tasks;
using ClothingStoreApi.DTO;
using ClothingStoreApi.Models;

namespace ClothingStoreApi.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(Order order);
        Task<List<Order>> GetUserOrders(int userId);
        Task<Order> DeleteOrder(int orderId);
        Task<Order> UpdateOrderStatus(OrderStatusUpdateDTO orderStatusUpdateDTO);
    }
}
