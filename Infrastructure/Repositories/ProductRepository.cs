using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase, IProductRepository
{
    public ProductRepository()
    {
        CreateTables();
    }

    public async Task AddProductAsync(Product product)
    {
        var insertQuery = "INSERT INTO Products (Name, Price, Description, Images) " +
                          "VALUES (@name, @price, @description, @images)";

        await using var command = new SqliteCommand(insertQuery, Connection);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@description", product.Description);
        command.Parameters.AddWithValue("@images", string.Join(",", product.Images));

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var updateQuery = "UPDATE Products SET Name = @name, Price = @price, " +
                          "Description = @description, Images = @images WHERE Id = @id";

        await using var command = new SqliteCommand(updateQuery, Connection);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@price", product.Price);
        command.Parameters.AddWithValue("@description", product.Description);
        command.Parameters.AddWithValue("@images", string.Join(",", product.Images));
        command.Parameters.AddWithValue("@id", product.Id);

        await command.ExecuteNonQueryAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var deleteQuery = "DELETE FROM Products WHERE Id = @id";

        await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
        deleteCommand.Parameters.AddWithValue("@id", id);
        await deleteCommand.ExecuteNonQueryAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var selectQuery = "SELECT * FROM Products WHERE Id = @productId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@productId", productId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var product = new Product(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetDecimal(2),
                reader.GetString(3),
                reader.GetString(4).Split(','));

            return product;
        }

        return null; // Return null if the product with the given ID is not found
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        var products = new List<Product>();
        var selectQuery = "SELECT * FROM Products";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var product = new Product(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetDecimal(2),
                reader.GetString(3),
                reader.GetString(4).Split(','));

            products.Add(product);
        }

        return products;
    }

    protected override void CreateTables()
    {
        var createTableQuery = "CREATE TABLE IF NOT EXISTS Products (" +
                               "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                               "Name TEXT NOT NULL, " +
                               "Price REAL NOT NULL, " +
                               "Description TEXT, " +
                               "Images TEXT)";

        using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
        createTableCommand.ExecuteNonQuery();
    }
}