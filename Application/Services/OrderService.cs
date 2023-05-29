using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new KeyNotFoundException();

        var user = await _userRepository.GetUserByIdAsync(order.UserId);
        if (user is not { Role: UserRole.Customer }) return order;

        order.Customer = (Customer)user;
        return order;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = await _orderRepository.GetAllOrdersAsync();

        foreach (var order in orders)
        {
            var user = await _userRepository.GetUserByIdAsync(order.UserId);
            if (user is not { Role: UserRole.Customer }) continue;
            order.Customer = (Customer)user;
            order.CalculateTotalAmount();
        }

        return orders;
    }

    public async Task<Order> AddOrderAsync(OrderDto orderDto)
    {
        return await _orderRepository.AddOrderAsync(orderDto);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        await _orderRepository.UpdateOrderAsync(order);
    }

    public async Task DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteOrderAsync(id);
    }
}