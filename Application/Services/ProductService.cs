using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

/// <summary>
/// Represents a service for managing products.
/// </summary>
public class ProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IScraper _scraper;

    /// <summary>
    /// Constructor for ProductService class.
    /// </summary>
    /// <param name="productRepository">An interface for product repositories.</param>
    /// <param name="scraper">An interface for product scrapers.</param>
    public ProductService(IProductRepository productRepository, IScraper scraper)
    {
        _productRepository = productRepository;
        _scraper = scraper;
    }

    /// <summary>
    /// Gets a product object by its id asynchronously.
    /// </summary>
    /// <param name="productId">A product's id.</param>
    /// <returns>A Task wrapping a Product object.</returns>
    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _productRepository.GetProductByIdAsync(productId);
    }

    /// <summary>
    /// Gets a list of all product objects asynchronously.
    /// </summary>
    /// <returns>A Task wrapping a List of Product objects.</returns>
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    /// <summary>
    /// Adds a product object asynchronously.
    /// </summary>
    /// <param name="product">A Product object.</param>
    /// <returns>The id of the Product object added.</returns>
    public async Task<int> AddProductAsync(Product product)
    {
        return await _productRepository.AddProductAsync(product);
    }

    /// <summary>
    /// Updates a product object asynchronously.
    /// </summary>
    /// <param name="product">A Product object.</param>
    /// <returns>A completed Task.</returns>
    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateProductAsync(product);
    }

    /// <summary>
    /// Deletes a product object by its id asynchronously.
    /// </summary>
    /// <param name="id">A product's id.</param>
    /// <returns>A completed Task.</returns>
    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteProductAsync(id);
    }

    /// <summary>
    /// Scrapes and adds a list of Product objects asynchronously.
    /// </summary>
    /// <returns>A completed Task.</returns>
    public async Task ScrapeAndAddProductsAsync()
    {
        var products = await _scraper.ScrapeProductsAsync();

        foreach (var product in products) await _productRepository.AddProductAsync(product);
    }

    /// <summary>
    /// Gets a list of product objects matching a search query asynchronously.
    /// </summary>
    /// <param name="searchQuery">A search query.</param>
    /// <returns>A Task wrapping a List of Product objects.</returns>
    public async Task<List<Product>> GetProductsBySearchQueryAsync(string searchQuery)
    {
        return await _productRepository.GetProductsBySearchQueryAsync(searchQuery);
    }
}
