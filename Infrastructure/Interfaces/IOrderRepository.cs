using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task AddOrderAsync(Order order);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetAllOrdersAsync();
}