using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await Task.Run(() => _orderRepository.GetOrderByIdAsync(orderId));
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await Task.Run(() => _orderRepository.GetAllOrdersAsync());
    }

    public async Task AddOrderAsync(Order order)
    {
        await Task.Run(() => _orderRepository.AddOrderAsync(order));
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await Task.Run(() => _orderRepository.UpdateOrderAsync(order));
    }

    public async Task DeleteOrderAsync(int id)
    {
        await Task.Run(() => _orderRepository.DeleteOrderAsync(id));
    }
}