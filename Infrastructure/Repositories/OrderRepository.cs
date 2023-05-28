using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase, IOrderRepository
{
    public OrderRepository()
    {
        CreateTables();
    }

    public async Task AddOrderAsync(Order order)
    {
        var insertQuery = "INSERT INTO Orders (OrderId, CustomerId, OrderDate, TotalAmount, IsCompleted) " +
                          "VALUES (@orderId, @customerId, @orderDate, @totalAmount, @isCompleted)";

        await using var command = new SqliteCommand(insertQuery, Connection);
        command.Parameters.AddWithValue("@orderId", order.Id);
        command.Parameters.AddWithValue("@customerId", order.Customer.Id);
        command.Parameters.AddWithValue("@orderDate", order.OrderDate);
        command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
        command.Parameters.AddWithValue("@isCompleted", order.IsCompleted ? 1 : 0);

        await command.ExecuteNonQueryAsync();

        // Save the products for the order
        await SaveOrderProductsAsync(order);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        var updateQuery = "UPDATE Orders SET CustomerId = @customerId, OrderDate = @orderDate, " +
                          "TotalAmount = @totalAmount, IsCompleted = @isCompleted WHERE OrderId = @orderId";

        await using var updateCommand = new SqliteCommand(updateQuery, Connection);
        updateCommand.Parameters.AddWithValue("@customerId", order.Customer.Id);
        updateCommand.Parameters.AddWithValue("@orderDate", order.OrderDate);
        updateCommand.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
        updateCommand.Parameters.AddWithValue("@isCompleted", order.IsCompleted ? 1 : 0);
        updateCommand.Parameters.AddWithValue("@orderId", order.Id);

        await updateCommand.ExecuteNonQueryAsync();

        // Save the updated products for the order
        await SaveOrderProductsAsync(order);
    }

    public async Task DeleteOrderAsync(int id)
    {
        var deleteQuery = "DELETE FROM Orders WHERE Id = @id";

        await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
        deleteCommand.Parameters.AddWithValue("@id", id);
        await deleteCommand.ExecuteNonQueryAsync();
    }


    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        var selectQuery = "SELECT * FROM Orders WHERE OrderId = @orderId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@orderId", orderId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var customerRepository = new UserRepository();
            var customer = await customerRepository.GetUserByIdAsync(reader.GetInt32(1));
            if (customer != null)
            {
                var products = await GetOrderProductsAsync(orderId);

                var order = new Order((Customer)customer, products)
                {
                    Id = reader.GetInt32(0),
                    OrderDate = reader.GetDateTime(3),
                    TotalAmount = reader.GetDecimal(4),
                    IsCompleted = reader.GetBoolean(5)
                };

                return order;
            }
        }

        return null; // Return null if the order with the given ID is not found
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = new List<Order>();
        var selectQuery = "SELECT * FROM Orders";

        await using var command = new SqliteCommand(selectQuery, Connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var customerRepository = new UserRepository();
            var customer = await customerRepository.GetUserByIdAsync(reader.GetInt32(1));
            if (customer != null)
            {
                var products = await GetOrderProductsAsync(reader.GetInt32(0));

                var order = new Order((Customer)customer, products)
                {
                    Id = reader.GetInt32(0),
                    OrderDate = reader.GetDateTime(3),
                    TotalAmount = reader.GetDecimal(4),
                    IsCompleted = reader.GetBoolean(5)
                };

                orders.Add(order);
            }
        }

        return orders;
    }

    protected override void CreateTables()
    {
        var createOrdersTableQuery = "CREATE TABLE IF NOT EXISTS Orders (" +
                                     "OrderId INTEGER PRIMARY KEY, " +
                                     "CustomerId INTEGER, " +
                                     "OrderDate TEXT, " +
                                     "TotalAmount DECIMAL(10, 2), " +
                                     "IsCompleted INTEGER)";

        using var createOrdersTableCommand = new SqliteCommand(createOrdersTableQuery, Connection);
        createOrdersTableCommand.ExecuteNonQuery();

        var createOrderProductsTableQuery = "CREATE TABLE IF NOT EXISTS OrderProducts (" +
                                            "OrderId INTEGER, " +
                                            "ProductId INTEGER, " +
                                            "FOREIGN KEY(OrderId) REFERENCES Orders(OrderId), " +
                                            "FOREIGN KEY(ProductId) REFERENCES Products(ProductId))";

        using var createOrderProductsTableCommand = new SqliteCommand(createOrderProductsTableQuery, Connection);
        createOrderProductsTableCommand.ExecuteNonQuery();
    }

    protected override void AddData()
    {
        throw new NotImplementedException();
    }

    private async Task SaveOrderProductsAsync(Order order)
    {
        // Delete existing order products
        var deleteQuery = "DELETE FROM OrderProducts WHERE OrderId = @orderId";

        await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
        deleteCommand.Parameters.AddWithValue("@orderId", order.Id);

        await deleteCommand.ExecuteNonQueryAsync();

        // Insert new order products
        var insertQuery = "INSERT INTO OrderProducts (OrderId, ProductId) VALUES (@orderId, @productId)";

        await using var insertCommand = new SqliteCommand(insertQuery, Connection);

        foreach (var product in order.Products)
        {
            insertCommand.Parameters.Clear();
            insertCommand.Parameters.AddWithValue("@orderId", order.Id);
            insertCommand.Parameters.AddWithValue("@productId", product.Id);

            await insertCommand.ExecuteNonQueryAsync();
        }
    }

    private async Task<List<Product>> GetOrderProductsAsync(int orderId)
    {
        var products = new List<Product>();
        var selectQuery = "SELECT * FROM OrderProducts WHERE OrderId = @orderId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@orderId", orderId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var productId = reader.GetInt32(1);
            var productRepository = new ProductRepository();
            var product = await productRepository.GetProductByIdAsync(productId);
            if (product != null) products.Add(product);
        }

        return products;
    }
}