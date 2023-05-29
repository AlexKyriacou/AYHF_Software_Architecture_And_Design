using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

public interface IOrderRepository
{
    Task<int> AddOrderAsync(OrderDto orderDto);
    Task UpdateOrderAsync(Order order);
    Task DeleteOrderAsync(int id);
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetAllOrdersByUserIdAsync(int userId);
}