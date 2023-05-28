using Microsoft.Data.Sqlite;
using MyProject.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Infrastructure.Repositories
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository() : base()
        {
            CreateTables();
        }

        protected override void CreateTables()
        {
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Products (" +
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

        public async Task AddProductAsync(Product product)
        {
            string insertQuery = "INSERT INTO Products (Name, Description, LongDescription, Ingredients, Image, Rating, NumRatings, Price) " +
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
            string updateQuery = "UPDATE Products SET Name = @name, Description = @description, " +
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
            string deleteQuery = "DELETE FROM Products WHERE Id = @id";

            await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
            deleteCommand.Parameters.AddWithValue("@id", id);
            await deleteCommand.ExecuteNonQueryAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            string selectQuery = "SELECT * FROM Products WHERE Id = @productId";

            await using var selectCommand = new SqliteCommand(selectQuery, Connection);
            selectCommand.Parameters.AddWithValue("@productId", productId);

            await using var reader = await selectCommand.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                Product product = new Product(
                    id: reader.GetInt32(0),
                    name: reader.GetString(1),
                    description: reader.GetString(2),
                    longDescription: reader.GetString(3),
                    ingredients: reader.GetString(4),
                    image: reader.GetString(5),
                    rating: reader.GetInt32(6),
                    numRatings: reader.GetInt32(7),
                    price: reader.GetDecimal(8));

                return product;
            }

            return null; // Return null if the product with the given ID is not found
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            List<Product> products = new List<Product>();
            string selectQuery = "SELECT * FROM Products";

            await using var selectCommand = new SqliteCommand(selectQuery, Connection);
            await using var reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                Product product = new Product(
                    id: reader.GetInt32(0),
                    name: reader.GetString(1),
                    description: reader.GetString(2),
                    longDescription: reader.GetString(3),
                    ingredients: reader.GetString(4),
                    image: reader.GetString(5),
                    rating: reader.GetInt32(6),
                    numRatings: reader.GetInt32(7),
                    price: reader.GetDecimal(8));

                products.Add(product);
            }

            return products;
        }
    }
}
