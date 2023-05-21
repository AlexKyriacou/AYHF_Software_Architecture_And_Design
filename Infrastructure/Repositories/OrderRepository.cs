using Microsoft.Data.Sqlite;
using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase
{
    private static OrderRepository? _instance;

    public OrderRepository()
    {
        _instance = this;
    }

    public static OrderRepository Instance
    {
        get
        {
            _instance ??= new OrderRepository();
            return _instance;
        }
    }

    public void AddOrder(Order order)
    {
        CreateTables();

        using (var transaction = Connection.BeginTransaction())
        {
            var command = Connection.CreateCommand();
            command.CommandText = "INSERT INTO Orders (OrderId, CustomerId, OrderDate, TotalAmount, IsCompleted) " +
                                  "VALUES (@orderId, @customerId, @orderDate, @totalAmount, @isCompleted)";
            command.Parameters.AddWithValue("@orderId", order.Id);
            command.Parameters.AddWithValue("@customerId", order.Customer.Id);
            command.Parameters.AddWithValue("@orderDate", order.OrderDate);
            command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
            command.Parameters.AddWithValue("@isCompleted", order.IsCompleted ? 1 : 0);

            command.ExecuteNonQuery();

            // Save the products for the order
            SaveOrderProducts(order);

            transaction.Commit();
        }
    }

    public void UpdateOrder(Order order)
    {
        CreateTables();

        using (var transaction = Connection.BeginTransaction())
        {
            var command = Connection.CreateCommand();
            command.CommandText = "UPDATE Orders SET CustomerId = @customerId, OrderDate = @orderDate, " +
                                  "TotalAmount = @totalAmount, IsCompleted = @isCompleted WHERE OrderId = @orderId";
            command.Parameters.AddWithValue("@customerId", order.Customer.Id);
            command.Parameters.AddWithValue("@orderDate", order.OrderDate);
            command.Parameters.AddWithValue("@totalAmount", order.TotalAmount);
            command.Parameters.AddWithValue("@isCompleted", order.IsCompleted ? 1 : 0);
            command.Parameters.AddWithValue("@orderId", order.Id);

            command.ExecuteNonQuery();

            // Save the updated products for the order
            SaveOrderProducts(order);

            transaction.Commit();
        }
    }

    public void DeleteOrder(Order order)
    {
        CreateTables();

        using (var transaction = Connection.BeginTransaction())
        {
            var command = Connection.CreateCommand();
            command.CommandText = "DELETE FROM Orders WHERE OrderId = @orderId";
            command.Parameters.AddWithValue("@orderId", order.Id);

            command.ExecuteNonQuery();

            transaction.Commit();
        }
    }


    public Order? GetOrderById(int orderId)
    {
        CreateTables();

        using (var command = new SqliteCommand("SELECT * FROM Orders WHERE OrderId = @orderId", Connection))
        {
            command.Parameters.AddWithValue("@orderId", orderId);

            using (var reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    Customer? customer = UserRepository.Instance.GetUserById<Customer>(reader.GetInt32(1));
                    if (customer != null)
                    {
                        List<Product> products = GetOrderProducts(orderId);

                        Order order = new(customer, products)
                        {
                            Id = reader.GetInt32(0),
                            OrderDate = reader.GetDateTime(3),
                            TotalAmount = reader.GetDecimal(4),
                            IsCompleted = reader.GetBoolean(5)
                        };

                        return order;
                    }
                }
            }
        }

        return null; // Return null if the order with the given ID is not found
    }

    public List<Order> GetAllOrders()
    {
        CreateTables();

        List<Order> orders = new List<Order>();

        using (var command = new SqliteCommand("SELECT * FROM Orders", Connection))
        {
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Customer? customer = UserRepository.Instance.GetUserById<Customer>(reader.GetInt32(1));
                    if (customer != null)
                    {
                        List<Product> products = GetOrderProducts(reader.GetInt32(0));

                        Order order = new(customer, products)
                        {
                            Id = reader.GetInt32(0),
                            OrderDate = reader.GetDateTime(3),
                            TotalAmount = reader.GetDecimal(4),
                            IsCompleted = reader.GetBoolean(5)
                        };

                        orders.Add(order);
                    }
                }
            }
        }

        return orders;
    }

    private void SaveOrderProducts(Order order)
    {
        // Delete existing order products
        using (var command = Connection.CreateCommand())
        {
            command.CommandText = "DELETE FROM OrderProducts WHERE OrderId = @orderId";
            command.Parameters.AddWithValue("@orderId", order.Id);

            command.ExecuteNonQuery();
        }

        // Insert new order products
        using (var command = Connection.CreateCommand())
        {
            command.CommandText = "INSERT INTO OrderProducts (OrderId, ProductId) VALUES (@orderId, @productId)";

            foreach (var product in order.Products)
            {
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@orderId", order.Id);
                command.Parameters.AddWithValue("@productId", product.Id);

                command.ExecuteNonQuery();
            }
        }
    }

    private List<Product> GetOrderProducts(int orderId)
    {
        List<Product> products = new();

        using (var command = new SqliteCommand("SELECT * FROM OrderProducts WHERE OrderId = @orderId", Connection))
        {
            command.Parameters.AddWithValue("@orderId", orderId);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int productId = reader.GetInt32(1);
                    Product? product = ProductRepository.Instance.GetProductById(productId);
                    if (product != null)
                    {
                        products.Add(product);
                    }
                }
            }
        }

        return products;
    }

    protected override void CreateTables()
    {
        // Create the Orders table if it doesn't exist
        using (var command = Connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS Orders (" +
                                  "OrderId INTEGER PRIMARY KEY, " +
                                  "CustomerId INTEGER, " +
                                  "OrderDate TEXT, " +
                                  "TotalAmount DECIMAL(10, 2), " +
                                  "IsCompleted INTEGER)";
            command.ExecuteNonQuery();
        }

        // Create the OrderProducts table if it doesn't exist
        using (var command = Connection.CreateCommand())
        {
            command.CommandText = "CREATE TABLE IF NOT EXISTS OrderProducts (" +
                                  "OrderId INTEGER, " +
                                  "ProductId INTEGER, " +
                                  "FOREIGN KEY(OrderId) REFERENCES Orders(OrderId), " +
                                  "FOREIGN KEY(ProductId) REFERENCES Products(ProductId))";
            command.ExecuteNonQuery();
        }
    }
}