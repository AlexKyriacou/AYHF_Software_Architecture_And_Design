using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IScraper _scraper;

    public ProductService(IProductRepository productRepository, IScraper scraper)
    {
        _productRepository = productRepository;
        _scraper = scraper;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetProductByIdAsync(productId);
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<int> AddProductAsync(Product product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateProductAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }

    public async Task ScrapeAndAddProductsAsync()
    {
        var products = await _scraper.ScrapeProductsAsync();

        foreach (var product in products) await _productRepository.AddProductAsync(product);
    }

    public async Task<List<Product>> GetProductsBySearchQueryAsync(string searchQuery)
    {
        return await _productRepository.GetProductsBySearchQueryAsync(searchQuery);
    }
}