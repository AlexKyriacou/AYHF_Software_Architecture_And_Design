using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Repositories;

namespace MyProject.Domain.Models;

public class StockReport : IReport
{
    private readonly ProductRepository _productRepository;

    public StockReport(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void GenerateReport()
    {
        // Logic to generate stock report using the ProductRepository
        Console.WriteLine("Generating stock report...");

        // Retrieve products from the repository and process them for reporting
        var products = _productRepository.GetAllProducts();

        // Perform necessary calculations and generate the report
        // ...

        Console.WriteLine("Stock report generated successfully.");
    }
}