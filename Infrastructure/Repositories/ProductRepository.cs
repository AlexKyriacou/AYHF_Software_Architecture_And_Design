using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class ProductRepository
{
    private readonly List<Product> _productList;

    public ProductRepository()
    {
        _productList = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        _productList.Add(product);
    }

    public void UpdateProduct(Product product)
    {
        // Update the product in the database
    }

    public void DeleteProduct(Product product)
    {
        _productList.Remove(product);
    }

    public Product GetProductById(int productId)
    {
        // Retrieve the product from the database by ID
        return _productList.Find(p => p.Id == productId);
    }

    public List<Product> GetAllProducts()
    {
        // Retrieve all products from the database
        return _productList;
    }
}