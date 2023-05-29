using AYHF_Software_Architecture_And_Design.Application.Dtos;
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

    public async Task<List<Order>> GetAllOrdersByUserIdAsync(int userId)
    {
        var orders = new List<Order>();
        var selectQuery = "SELECT * FROM Orders WHERE userId = @userId";

        await using var command = new SqliteCommand(selectQuery, Connection);
        command.Parameters.AddWithValue("@userId", userId);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var order = new Order
            {
                Id = reader.GetInt32(0),
                OrderDate = reader.GetDateTime(2),
                TotalAmount = reader.GetDecimal(3),
                IsCompleted = reader.GetBoolean(4)
            };

            orders.Add(order);
        }

        return orders;
    }

    public async Task<Order> AddOrderAsync(OrderDto orderDto)
    {
        var order = new Order
        {
            UserId = orderDto.UserId,
            Products = orderDto.Products
        };

        order.CalculateTotalAmount();

        var insertQuery = "INSERT INTO Orders (userId, OrderDate, TotalAmount, IsCompleted) " +
                          "VALUES (@userId, @orderDate, @totalAmount, @isCompleted);" +
                          "SELECT last_insert_rowid();";

        await using var command = new SqliteCommand(insertQuery, Connection);
        command.Parameters.AddWithValue("@userId", order.UserId);
        command.Parameters.AddWithValue("@orderDate", order.OrderDate);
        command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
        command.Parameters.AddWithValue("@isCompleted", order.IsCompleted ? 1 : 0);

        var orderId = Convert.ToInt32(await command.ExecuteScalarAsync());
        order.Id = orderId;
        await SaveOrderProductsAsync(order);

        return order;
    }

    public async Task UpdateOrderAsync(Order order)
    {
        var updateQuery = "UPDATE Orders SET userId = @userId, OrderDate = @orderDate, " +
                          "TotalAmount = @totalAmount, IsCompleted = @isCompleted WHERE OrderId = @orderId";

        await using var updateCommand = new SqliteCommand(updateQuery, Connection);
        updateCommand.Parameters.AddWithValue("@userId", order.UserId);
        updateCommand.Parameters.AddWithValue("@orderDate", order.OrderDate);
        updateCommand.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
        updateCommand.Parameters.AddWithValue("@isCompleted", order.IsCompleted ? 1 : 0);
        updateCommand.Parameters.AddWithValue("@orderId", order.Id);

        await updateCommand.ExecuteNonQueryAsync();
        await SaveOrderProductsAsync(order);
    }

    public async Task DeleteOrderAsync(int id)
    {
        var deleteQuery = "DELETE FROM Orders WHERE OrderId = @id";

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
            var productIds = await GetOrderProductIdsAsync(orderId);

            var order = new Order
            {
                Id = reader.GetInt32(0),
                OrderDate = reader.GetDateTime(2),
                TotalAmount = reader.GetDecimal(3),
                IsCompleted = reader.GetBoolean(4),
                UserId = reader.GetInt32(1),
                Products = await GetOrderProductsAsync(productIds)
            };

            return order;
        }

        return null;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var orders = new List<Order>();
        var selectQuery = "SELECT * FROM Orders";

        await using var command = new SqliteCommand(selectQuery, Connection);
        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var productIds = await GetOrderProductIdsAsync(reader.GetInt32(0));

            var order = new Order
            {
                Id = reader.GetInt32(0),
                OrderDate = reader.GetDateTime(2),
                TotalAmount = reader.GetDecimal(3),
                IsCompleted = reader.GetBoolean(4),
                UserId = reader.GetInt32(1),
                Products = await GetOrderProductsAsync(productIds)
            };

            orders.Add(order);
        }

        return orders;
    }

    private async Task<List<int>> GetOrderProductIdsAsync(int orderId)
    {
        var productIds = new List<int>();
        var selectQuery = "SELECT ProductId FROM OrderProducts WHERE OrderId = @orderId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@orderId", orderId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync()) productIds.Add(reader.GetInt32(0));

        return productIds;
    }

    private async Task<List<Product>> GetOrderProductsAsync(List<int> productIds)
    {
        var products = new List<Product>();

        var inClause = string.Join(",", productIds);

        var selectQuery = $"SELECT * FROM Products WHERE Id IN ({inClause})";

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
                reader.GetDecimal(8)
            );

            products.Add(product);
        }

        return products;
    }

    protected override void CreateTables()
    {
        var createOrdersTableQuery = "CREATE TABLE IF NOT EXISTS Orders (" +
                                     "OrderId INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                     "userId INTEGER, " +
                                     "OrderDate TEXT, " +
                                     "TotalAmount DECIMAL(10, 2), " +
                                     "IsCompleted INTEGER)";

        using var createOrdersTableCommand = new SqliteCommand(createOrdersTableQuery, Connection);
        createOrdersTableCommand.ExecuteNonQuery();

        var createOrderProductsTableQuery = "CREATE TABLE IF NOT EXISTS OrderProducts (" +
                                            "OrderId INTEGER, " +
                                            "ProductId INTEGER, " +
                                            "PRIMARY KEY (OrderId, ProductId))";

        using var createOrderProductsTableCommand = new SqliteCommand(createOrderProductsTableQuery, Connection);
        createOrderProductsTableCommand.ExecuteNonQuery();

        var createDeliveryAddressesTableQuery = "CREATE TABLE IF NOT EXISTS DeliveryAddresses (" +
                                                "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                                "userId INTEGER, " +
                                                "Street TEXT, " +
                                                "City TEXT, " +
                                                "State TEXT, " +
                                                "PostalCode TEXT)";

        using var createDeliveryAddressesTableCommand =
            new SqliteCommand(createDeliveryAddressesTableQuery, Connection);
        createDeliveryAddressesTableCommand.ExecuteNonQuery();
    }

    private async Task SaveOrderProductsAsync(Order order)
    {
        var insertQuery =
            "INSERT OR IGNORE INTO OrderProducts (OrderId, ProductId) VALUES (@orderId, @productId)"; // Change 'ProductId' to 'Id'

        await using var insertCommand = new SqliteCommand(insertQuery, Connection);

        foreach (var product in order.Products)
        {
            insertCommand.Parameters.Clear();
            insertCommand.Parameters.AddWithValue("@orderId", order.Id);
            insertCommand.Parameters.AddWithValue("@productId", product.Id);

            await insertCommand.ExecuteNonQueryAsync();
        }
    }
}