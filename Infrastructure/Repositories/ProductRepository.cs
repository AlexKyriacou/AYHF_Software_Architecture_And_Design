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
        var insertQuery =
            "INSERT INTO Products (Name, Description, LongDescription, Ingredients, Image, Rating, NumRatings, Price) " +
            "VALUES (@name, @description, @longDescription, @ingredients, @image, @rating, @numRatings, @price)";

        await using var command = new SqliteCommand(insertQuery, Connection);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@description", product.Description);
        command.Parameters.AddWithValue("@longDescription", product.LongDescription);
        command.Parameters.AddWithValue("@ingredients", product.Ingredients);
        command.Parameters.AddWithValue("@image", product.Image);
        command.Parameters.AddWithValue("@rating", product.Rating);
        command.Parameters.AddWithValue("@numRating", product.NumRatings);
        command.Parameters.AddWithValue("@price", product.Price);

        await command.ExecuteNonQueryAsync();
    }

    public async Task UpdateProductAsync(Product product)
    {
        var updateQuery = "UPDATE Products SET Name = @name, Description = @description, " +
                          "LongDescription = @longDescription, Ingredients = @ingredients, " +
                          "Image = @image, Rating = @rating, NumRatings = @numRatings, " +
                          "Price = @price WHERE Id = @id";

        await using var command = new SqliteCommand(updateQuery, Connection);
        command.Parameters.AddWithValue("@name", product.Name);
        command.Parameters.AddWithValue("@description", product.Description);
        command.Parameters.AddWithValue("@longDescription", product.LongDescription);
        command.Parameters.AddWithValue("@ingredients", product.Ingredients);
        command.Parameters.AddWithValue("@image", product.Image);
        command.Parameters.AddWithValue("@rating", product.Rating);
        command.Parameters.AddWithValue("@numRating", product.NumRatings);
        command.Parameters.AddWithValue("@price", product.Price);

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
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetInt32(6),
                reader.GetInt32(7),
                reader.GetDecimal(8));

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
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4),
                reader.GetString(5),
                reader.GetInt32(6),
                reader.GetInt32(7),
                reader.GetDecimal(8));

            products.Add(product);
        }

        return products;
    }

    protected override void CreateTables()
    {
        var createTableQuery = "CREATE TABLE IF NOT EXISTS Products (" +
                               "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                               "Name TEXT NOT NULL, " +
                               "Description TEXT, " +
                               "LongDescription TEXT, " +
                               "Ingredients TEXT, " +
                               "Image TEXT, " +
                               "Rating INTEGER, " +
                               "NumRatings INTEGER, " +
                               "Price REAL NOT NULL)";

        using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
        createTableCommand.ExecuteNonQuery();
    }
}