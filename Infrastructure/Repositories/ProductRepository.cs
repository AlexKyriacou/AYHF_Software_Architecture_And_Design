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
                                      "Price REAL NOT NULL, " +
                                      "Description TEXT, " +
                                      "Images TEXT)";

            using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
            createTableCommand.ExecuteNonQuery();
        }

        public async Task AddProductAsync(Product product)
        {
            string insertQuery = "INSERT INTO Products (Name, Price, Description, Images) " +
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
            string updateQuery = "UPDATE Products SET Name = @name, Price = @price, " +
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
                    price: reader.GetDecimal(2),
                    description: reader.GetString(3),
                    images: reader.GetString(4).Split(','));

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
                    price: reader.GetDecimal(2),
                    description: reader.GetString(3),
                    images: reader.GetString(4).Split(','));

                products.Add(product);
            }

            return products;
        }
    }
}
