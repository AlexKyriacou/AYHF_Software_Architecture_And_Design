using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Repositories;

namespace MyProject.Domain.Models;

public class SalesReport : IReport
{
    private readonly OrderRepository _orderRepository;

    public SalesReport(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public void GenerateReport()
    {
        // Logic to generate sales report using the OrderRepository
        Console.WriteLine("Generating sales report...");

        // Retrieve orders from the repository and process them for reporting
        var orders = _orderRepository.GetAllOrders();

        // Perform necessary calculations and generate the report
        // ...

        Console.WriteLine("Sales report generated successfully.");
    }
}