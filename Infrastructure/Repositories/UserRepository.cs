using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository()
    {
        CreateTables();
    }

    public async Task<IUser?> GetUserByIdAsync(int id)
    {
        var selectQuery = @"
    SELECT Users.*, DeliveryAddresses.*
    FROM Users 
    LEFT JOIN DeliveryAddresses ON Users.Id = DeliveryAddresses.UserId AND Users.Role = 'Customer'
    WHERE Users.Id = @id";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@id", id);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        IUser user = null;

        while (await reader.ReadAsync())
        {
            var role = (UserRole)reader.GetInt32(5);

            user ??= role switch
            {
                UserRole.Customer => new Customer
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Username = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    DeliveryAddress = !reader.IsDBNull(6)
                        ? new DeliveryAddress(
                            reader.GetString(6),
                            reader.IsDBNull(7) ? null : reader.GetString(7),
                            reader.IsDBNull(8) ? null : reader.GetString(8),
                            reader.IsDBNull(9) ? null : reader.GetString(9)
                        )
                        : null,
                    Role = role
                },
                UserRole.Admin => new Admin
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Username = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    Role = role
                },
                _ => throw new Exception("Invalid user role.")
            };
        }

        return user;
    }

    public async Task<List<IUser>> GetUsersAsync()
    {
        var users = new List<IUser>();
        var selectQuery = "SELECT * FROM Users";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            IUser user = (UserRole)reader.GetInt32(5) switch
            {
                UserRole.Customer => new Customer(),
                UserRole.Admin => new Admin(),
                _ => throw new Exception("Invalid user role.")
            };
            user.Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0;
            user.Name = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty;
            user.Username = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty;
            user.Email = !reader.IsDBNull(4) ? reader.GetString(3) : string.Empty;
            user.Password = !reader.IsDBNull(3) ? reader.GetString(4) : string.Empty;
            user.Role = !reader.IsDBNull(5) ? (UserRole)reader.GetInt32(5) : UserRole.Customer;
            users.Add(user);
        }

        return users;
    }

    public async Task<IUser?> GetUserByEmailAsync(string email)
    {
        IUser? user = null;
        var selectQuery = "SELECT * FROM Users WHERE Email = @email";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@email", email);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var role = (UserRole)reader.GetInt32(5);
            user ??= role switch
            {
                UserRole.Customer => new Customer
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Username = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    DeliveryAddress = !reader.IsDBNull(6)
                        ? new DeliveryAddress(
                            reader.GetString(6),
                            reader.IsDBNull(7) ? null : reader.GetString(7),
                            reader.IsDBNull(8) ? null : reader.GetString(8),
                            reader.IsDBNull(9) ? null : reader.GetString(9)
                        )
                        : null,
                    Role = role
                },
                UserRole.Admin => new Admin
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Username = reader.GetString(2),
                    Email = reader.GetString(3),
                    Password = reader.GetString(4),
                    Role = role
                },
                _ => throw new Exception("Invalid user role.")
            };
        }

        return user;
    }


    public async Task<int> AddUserAsync(IUser user)
    {
        var insertQuery =
            "INSERT INTO Users (Name, Username, Email, Password, Role) VALUES (@name, @username, @email, @password, @role);" +
            "SELECT last_insert_rowid();";
        await using var insertCommand = new SqliteCommand(insertQuery, Connection);
        insertCommand.Parameters.AddWithValue("@name", user.Name);
        insertCommand.Parameters.AddWithValue("@username", user.Username);
        insertCommand.Parameters.AddWithValue("@password", user.Password);
        insertCommand.Parameters.AddWithValue("@email", user.Email);
        insertCommand.Parameters.AddWithValue("@role", user.Role);

        var userId = Convert.ToInt32(await insertCommand.ExecuteScalarAsync());
        return userId;
    }

    public async Task UpdateUserAsync(IUser user)
    {
        var updateQuery =
            "UPDATE Users SET Username = @username, Password = @password, Email = @email WHERE Id = @userId";

        await using var updateCommand = new SqliteCommand(updateQuery, Connection);
        updateCommand.Parameters.AddWithValue("@name", user.Name);
        updateCommand.Parameters.AddWithValue("@username", user.Username);
        updateCommand.Parameters.AddWithValue("@password", user.Password);
        updateCommand.Parameters.AddWithValue("@email", user.Email);
        updateCommand.Parameters.AddWithValue("@userId", user.Id);

        await updateCommand.ExecuteNonQueryAsync();
    }

    public async Task DeleteUserAsync(int id)
    {
        var deleteQuery = "DELETE FROM Users WHERE Id = @userId";

        await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
        deleteCommand.Parameters.AddWithValue("@userId", id);
        await deleteCommand.ExecuteNonQueryAsync();
    }

    private async Task<DeliveryAddress> GetDeliveryAddressByUserId(int userId)
    {
        DeliveryAddress deliveryAddress = null;
        var selectQuery = "SELECT * FROM DeliveryAddresses WHERE UserId = @userId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@userId", userId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
            deliveryAddress = new DeliveryAddress(
                reader.GetString(1),
                reader.GetString(2),
                reader.GetString(3),
                reader.GetString(4)
            );

        return deliveryAddress;
    }

    private async Task<List<Order>> GetOrdersByUserId(Customer customer)
    {
        var orders = new List<Order>();
        var selectQuery = "SELECT * FROM Orders WHERE UserId = @userId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@userId", customer.Id);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var order = new Order
            {
                Id = reader.GetInt32(0),
                OrderDate = reader.GetDateTime(2),
                TotalAmount = reader.GetDecimal(3),
                IsCompleted = reader.GetBoolean(4),
                Customer = customer,
                Products = new List<Product>()
            };
            order.Customer = customer;
            order.Products = await GetProductsByOrderId(order.Id);
            orders.Add(order);
        }

        return orders;
    }

    private async Task<List<Product>> GetProductsByOrderId(int orderId)
    {
        var products = new List<Product>();
        var selectQuery = @"
    SELECT Products.*
    FROM OrderProducts
    JOIN Products ON OrderProducts.ProductId = Products.Id
    WHERE OrderProducts.OrderId = @orderId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@orderId", orderId);

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
        var createTableQuery =
            "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Username TEXT, Email TEXT, Password TEXT, Role TEXT)";
        using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
        createTableCommand.ExecuteNonQuery();
    }
}