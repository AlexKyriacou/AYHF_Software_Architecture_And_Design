using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

/// <summary>
/// Represents an interface for managing order entities in the system.
/// </summary>
/// <remarks>
/// Every method except <see cref="GetOrderByIdAsync"/> returns a Task tailed with an operation complete result.
/// </remarks>
public interface IOrderRepository
{
    /// <summary>
    /// Adds a new order entity to the system.
    /// </summary>
    /// <param name="orderDto">The orderDto containing details of the order to be added.</param>
    /// <returns>Careates and returns a new Task tailed with an operation complete status.</returns>
    Task<Order> AddOrderAsync(OrderDto orderDto);

    /// <summary>
    /// Updates an existing order entity in the system.
    /// </summary>
    /// <param name="order">The order to be updated.</param>
    /// <returns>Careates and returns a new Task tailed with an operation complete status..</returns>
    Task UpdateOrderAsync(Order order);

    /// <summary>
    /// Deletes an existing order entity from the system using its identifier.
    /// </summary>
    /// <param name="id">The int specifying the identifier of the order to be deleted.</param>
    /// <returns>Careates and returns a new Task tailed with the operation complete status.</returns>
    Task DeleteOrderAsync(int id);

    /// <summary>
    /// Gets an existing order entity from the system using its identifier.
    /// </summary>
    /// <param name="orderId">The int specifying the identifier of the order to be retrieved.</param>
    /// <returns>Returns a task tailored with a nullable Order entity.</returns>
    Task<Order?> GetOrderByIdAsync(int orderId);

    /// <summary>
    /// Gets all order entities in the system.
    /// </summary>
    /// <returns>A Task containing a list of all orders in the system.</returns>
    Task<List<Order>> GetAllOrdersAsync();

    /// <summary>
    /// Gets all order entities in the system associated with a specific user.
    /// </summary>
    /// <param name="userId">The int specifying the identifier of the user associated with the orders to be retrieved.</param>
    /// <returns>A Task containing a list of all orders associated with the user.</returns>
    Task<List<Order>> GetAllOrdersByUserIdAsync(int userId);
}