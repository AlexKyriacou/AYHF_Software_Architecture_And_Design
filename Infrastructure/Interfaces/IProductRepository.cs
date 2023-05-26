using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<Product?> GetProductByIdAsync(int productId);
        Task<List<Product>> GetAllProductsAsync();
    }
}