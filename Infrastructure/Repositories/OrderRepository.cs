using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class OrderRepository
{
    private readonly List<Order> _orders;

    public OrderRepository()
    {
        _orders = new List<Order>();
    }

    public void AddOrder(Order order)
    {
        _orders.Add(order);
    }

    public void UpdateOrder(Order order)
    {
        // Update the order in the database
    }

    public void DeleteOrder(Order order)
    {
        _orders.Remove(order);
    }

    public Order GetOrderById(int orderId)
    {
        // Retrieve the order from the database by ID
        return _orders.Find(o => o.Id == orderId);
    }

    public List<Order> GetAllOrders()
    {
        // Retrieve all orders from the database
        return _orders;
    }
}