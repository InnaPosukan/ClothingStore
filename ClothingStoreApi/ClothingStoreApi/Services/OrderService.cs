using System;
using System.Threading.Tasks;
using ClothingStoreApi.DBContext;
using ClothingStoreApi.DTO;
using ClothingStoreApi.Interfaces;
using ClothingStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClothingStoreApi.Services
{
    public class OrderService : IOrderService
    {
        private readonly ClothingStoreContext _dbContext;

        public OrderService(ClothingStoreContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            var user = await _dbContext.Users.FindAsync(order.UserId);
            var ad = await _dbContext.Advertisements.FindAsync(order.AdId);
            if (user == null || ad == null)
            {
                throw new ArgumentException("Пользователь или объявление не найдены.");
            }

            order.OrderDate = DateTime.Now;

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetUserOrders(int userId)
        {
            var userOrders = await _dbContext.Orders
                .Include(o => o.Ad)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            if (userOrders.Count == 0)
            {
                throw new ArgumentException("У пользователя еще нет заказов.");

            }

            return userOrders;
        }
        public async Task<Order> DeleteOrder(int orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new ArgumentException("Заказ не найден.");
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();

            return order;
        }
        public async Task<Order> UpdateOrderStatus(OrderStatusUpdateDTO orderStatusUpdateDTO)
        {
            var order = await _dbContext.Orders.FindAsync(orderStatusUpdateDTO.OrderId);
            if (order == null)
            {
                throw new ArgumentException("Заказ не найден.");
            }

            order.Status = orderStatusUpdateDTO.NewStatus;
            await _dbContext.SaveChangesAsync();

            return order;
        }

    }
}
 
