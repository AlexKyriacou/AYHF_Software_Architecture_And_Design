using Microsoft.Data.Sqlite;
using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase
{
    private static ProductRepository? _instance;

    public ProductRepository()
    {
        _instance = this;
    }

    public static ProductRepository Instance
    {
        get
        {
            _instance ??= new ProductRepository();
            return _instance;
        }
    }

    public void AddProduct(Product product)
    {
        CreateTables();

        using (var transaction = Connection.BeginTransaction())
        {
            var command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO Products (Id, Name, Price, Description, Images) VALUES (@id, @name, @price, @description, @images)";
            command.Parameters.AddWithValue("@id", product.Id);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@images", string.Join(",", product.Images));

            command.ExecuteNonQuery();

            transaction.Commit();
        }
    }

    public void UpdateProduct(Product product)
    {
        CreateTables();

        using (var transaction = Connection.BeginTransaction())
        {
            var command = Connection.CreateCommand();
            command.CommandText = "UPDATE Products SET Name = @name, Price = @price, Description = @description, Images = @images WHERE Id = @id";
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@images", string.Join(",", product.Images));
            command.Parameters.AddWithValue("@id", product.Id);

            command.ExecuteNonQuery();

            transaction.Commit();
        }
    }

    public void DeleteProduct(Product product)
    {
        CreateTables();

        using (var transaction = Connection.BeginTransaction())
        {
            var command = Connection.CreateCommand();
            command.CommandText = "DELETE FROM Products WHERE Id = @id";
            command.Parameters.AddWithValue("@id", product.Id);

            command.ExecuteNonQuery();

            transaction.Commit();
        }
    }

    public Product? GetProductById(int productId)
    {
        CreateTables();

        string selectQuery = "SELECT * FROM Products WHERE Id = @productId";
        using (var selectCommand = new SqliteCommand(selectQuery, Connection))
        {
            selectCommand.Parameters.AddWithValue("@productId", productId);

            using (var reader = selectCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    Product product = new Product(
                        id: reader.GetInt32(0),
                        name: reader.GetString(1),
                        price: reader.GetDecimal(2),
                        description: reader.GetString(3),
                        images: reader.GetString(4).Split(','));

                    return product;
                }
            }
        }

        return null; // Return null if the product with the given ID is not found
    }

    public List<Product> GetAllProducts()
    {
        CreateTables();

        List<Product> products = new List<Product>();

        string selectQuery = "SELECT * FROM Products";
        using (var selectCommand = new SqliteCommand(selectQuery, Connection))
        {
            using (var reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product(
                        id: reader.GetInt32(0),
                        name: reader.GetString(1),
                        price: reader.GetDecimal(2),
                        description: reader.GetString(3),
                        images: reader.GetString(4).Split(','));

                    products.Add(product);
                }
            }
        }

        return products;
    }

    protected override void CreateTables()
    {
        string createTableQuery = @"
                    CREATE TABLE IF NOT EXISTS Products (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Price REAL NOT NULL,
                        Description TEXT,
                        Images TEXT
                    )";
        using (var createTableCommand = new SqliteCommand(createTableQuery, Connection))
        {
            createTableCommand.ExecuteNonQuery();
        }
    }
}