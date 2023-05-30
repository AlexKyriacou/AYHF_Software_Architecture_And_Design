using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

/// <summary>
/// Represents an interface for managing products in the system.
/// </summary>
/// <remarks>
/// Every method except <see cref="GetProductByIdAsync"/> returns a Task tailed with an operation complete result.
/// </remarks>
public interface IProductRepository
{
    /// <summary>
    /// Adds a new product entity to the system.
    /// </summary>
    /// <param name="product">The product entity to be added.</param>
    /// <returns>A Task containing an integer representing the result of the operation.</returns>
    Task<int> AddProductAsync(Product product);

    /// <summary>
    /// Updates an existing product entity in the system.
    /// </summary>
    /// <param name="product">The product entity to be updated.</param>
    /// <returns>A Task containing an integer representing the result of the operation.</returns>
    Task UpdateProductAsync(Product product);

    /// <summary>
    /// Deletes an existing product entity from the system using its identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the product to be deleted.</param>
    /// <returns>A Task containing an integer representing the result of the operation.</returns>
    Task DeleteProductAsync(int id);

    /// <summary>
    /// Gets an existing product entity from the system using its identifier.
    /// </summary>
    /// <param name="productId">The integer identifier of the product entity to be retrieved.</param>
    /// <returns>A Task containing a nullable product entity.</returns>
    Task<Product?> GetProductByIdAsync(int productId);

    /// <summary>
    /// Gets all product entities in the system.
    /// </summary>
    /// <returns>A Task containing a list of all product entities in the system.</returns>
    Task<List<Product>> GetAllProductsAsync();

    /// <summary>
    /// Gets all product entities in the system associated with a search query.
    /// </summary>
    /// <param name="searchQuery">The search query used to filter the product entities.</param>
    /// <returns>A Task containing a list of all product entities associated with the search query.</returns>
    Task<List<Product>> GetProductsBySearchQueryAsync(string searchQuery);
}
