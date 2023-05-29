using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

public interface IProductRepository
{
    Task<int> AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(int id);
    Task<Product?> GetProductByIdAsync(int productId);
    Task<List<Product>> GetAllProductsAsync();
    Task<List<Product>> GetProductsBySearchQueryAsync(string searchQuery);

}