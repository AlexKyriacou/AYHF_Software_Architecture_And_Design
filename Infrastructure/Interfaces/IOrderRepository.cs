using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
    }
}