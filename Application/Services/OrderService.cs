using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

/// <summary>
/// Represents a service for managing orders.
/// </summary>
public class OrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor for OrderService class.
    /// </summary>
    /// <param name="orderRepository">An interface for order repositories.</param>
    /// <param name="userRepository">An interface for user repositories.</param>
    public OrderService(IOrderRepository orderRepository, IUserRepository userRepository)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
    }

    /// <summary>
    /// Gets an order object by its id asynchronously.
    /// </summary>
    /// <param name="orderId">An order's id.</param>
    /// <returns>A Task wrapping an Order object.</returns>
    /// <exception cref="KeyNotFoundException">Throws exception if order is not found.</exception>
    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        var order = await _orderRepository.GetOrderByIdAsync(orderId) ?? throw new KeyNotFoundException();

        var user = await _userRepository.GetUserByIdAsync(order.UserId);
        if (user is not { Role: UserRole.Customer }) return order;

        order.Customer = (Customer)user;
        return order;
    }

    /// <summary>
    /// Gets a list of all order objects asynchronously.
    /// </summary>
    /// <returns>A Task wrapping a List of Order objects.</returns>
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

    /// <summary>
    /// Adds an order object asynchronously.
    /// </summary>
    /// <param name="orderDto">A DTO object representing an order.</param>
    /// <returns>A Task wrapping an Order object.</returns>
    public async Task<Order> AddOrderAsync(OrderDto orderDto)
    {
        return await _orderRepository.AddOrderAsync(orderDto);
    }

    /// <summary>
    /// Updates an order object asynchronously.
    /// </summary>
    /// <param name="order">An Order object.</param>
    /// <returns>A completed Task.</returns>
    public async Task UpdateOrderAsync(Order order)
    {
        await _orderRepository.UpdateOrderAsync(order);
    }

    /// <summary>
    /// Deletes an order object by its id asynchronously.
    /// </summary>
    /// <param name="id">An order's id.</param>
    /// <returns>A completed Task.</returns>
    public async Task DeleteOrderAsync(int id)
    {
        await _orderRepository.DeleteOrderAsync(id);
    }
}
