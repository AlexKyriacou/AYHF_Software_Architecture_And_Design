using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await Task.Run(() => _productRepository.GetProductByIdAsync(productId));
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await Task.Run(() => _productRepository.GetAllProductsAsync());
    }

    public async Task AddProductAsync(Product product)
    {
        await Task.Run(() => _productRepository.AddProductAsync(product));
    }

    public async Task UpdateProductAsync(Product product)
    {
        await Task.Run(() => _productRepository.UpdateProductAsync(product));
    }

    public async Task DeleteProductAsync(int id)
    {
        await Task.Run(() => _productRepository.DeleteProductAsync(id));
    }
}