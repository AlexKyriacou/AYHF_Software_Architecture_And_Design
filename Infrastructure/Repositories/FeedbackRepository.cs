using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;


public class FeedbackRepository : RepositoryBase, IFeedbackRepository
{
    public FeedbackRepository()
    {
        CreateTables();
    }

    public async Task<int> AddFeedbackAsync(Feedback feedback)
    {
        var insertQuery = "INSERT INTO Feedbacks (CustomerId, ProductId, Rating, Message, FeedbackDate) VALUES (@customerId, @productId, @rating, @message, @feedbackDate); SELECT last_insert_rowid();";

        await using var command = new SqliteCommand(insertQuery, Connection);
        command.Parameters.AddWithValue("@customerId", feedback.CustomerId);
        command.Parameters.AddWithValue("@productId", feedback.ProductId);
        command.Parameters.AddWithValue("@rating", feedback.Rating);
        command.Parameters.AddWithValue("@message", feedback.Message);
        command.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);

        return Convert.ToInt32(await command.ExecuteScalarAsync());
    }


    public async Task UpdateFeedbackAsync(Feedback feedback)
    {
        var updateQuery =
            "UPDATE Feedbacks SET CustomerId = @customerId, ProductId = @productId, Rating = @rating, Message = @message, FeedbackDate = @feedbackDate WHERE FeedbackId = @feedbackId";

        await using var updateCommand = new SqliteCommand(updateQuery, Connection);
        updateCommand.Parameters.AddWithValue("@customerId", feedback.CustomerId);
        updateCommand.Parameters.AddWithValue("@productId", feedback.ProductId);
        updateCommand.Parameters.AddWithValue("@rating", feedback.Rating);
        updateCommand.Parameters.AddWithValue("@message", feedback.Message);
        updateCommand.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);
        updateCommand.Parameters.AddWithValue("@feedbackId", feedback.Id);

        await updateCommand.ExecuteNonQueryAsync();
    }

    public async Task DeleteFeedbackAsync(Feedback feedback)
    {
        await this.DeleteFeedbackAsync(feedback.Id);
    }

    public async Task DeleteFeedbackAsync(int Id)
    {
        var deleteQuery = "DELETE FROM Feedbacks WHERE FeedbackId = @feedbackId";

        await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
        deleteCommand.Parameters.AddWithValue("@feedbackId", Id);
        await deleteCommand.ExecuteNonQueryAsync();
    }

    public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
    {
        var selectQuery = "SELECT CustomerId, ProductId, Rating, Message, FeedbackDate FROM Feedbacks WHERE FeedbackId = @feedbackId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@feedbackId", feedbackId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var customerId = reader.GetInt32(0);
            var productId = reader.GetInt32(1);
            var rating = reader.GetInt32(2);
            var message = reader.GetString(3);
            var feedbackDate = reader.GetString(4);
            var customerRepository = new UserRepository();
            var customer = await customerRepository.GetUserByIdAsync(customerId);
            if (customer != null)
            {
                // Create a new Feedback object using the retrieved data
                var feedback = new Feedback(feedbackId, customer.Id, productId, rating, message, DateTime.Parse(feedbackDate));
                return feedback;
            }
        }

        return null; // Feedback not found
    }

    public async Task<List<Feedback>> GetAllFeedbackAsync()
    {
        var feedbacks = new List<Feedback>();
        var sql = "SELECT FeedbackId, CustomerId, ProductId, Rating, Message, FeedbackDate FROM Feedbacks ORDER BY FeedbackDate DESC";

        await using var command = new SqliteCommand(sql, Connection);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var feedbackId = reader.GetInt32(0);
            var customerId = reader.GetInt32(1);
            var productId = reader.GetInt32(2);
            var rating = reader.GetInt32(3);
            var message = reader.GetString(4);
            var feedbackDateString = reader.GetString(5);
            var customerRepository = new UserRepository();
            IUser? customer = await customerRepository.GetUserByIdAsync(customerId);
            if (customer != null)
            {
                var feedback = new Feedback(feedbackId, customer.Id, productId, rating, message,
                    DateTime.Parse(feedbackDateString));
                feedbacks.Add(feedback);
            }
        }

        return feedbacks;
    }
    public async Task<List<Feedback>> GetAllFeedbackForProductAsync(int productId)
    {
        var feedbacks = new List<Feedback>();
        var sql = "SELECT FeedbackId, CustomerId, Rating, Message, FeedbackDate FROM Feedbacks WHERE ProductId = @productId ORDER BY FeedbackDate DESC";

        await using var command = new SqliteCommand(sql, Connection);
        command.Parameters.AddWithValue("@productId", productId);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var feedbackId = reader.GetInt32(0);
            var customerId = reader.GetInt32(1);
            var rating = reader.GetInt32(2);
            var message = reader.GetString(3);
            var feedbackDateString = reader.GetString(4);
            var customerRepository = new UserRepository();
            IUser? customer = await customerRepository.GetUserByIdAsync(customerId);
            if (customer != null)
            {
                var feedback = new Feedback(feedbackId, customer.Id, productId, rating, message,
                    DateTime.Parse(feedbackDateString));
                feedbacks.Add(feedback);
            }
        }

        return feedbacks;
    }

    public async Task<float> GetAverageProductRatingAsync(int productId)
    {
        var selectQuery = "SELECT AVG(Rating) FROM Feedbacks WHERE ProductId = @productId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@productId", productId);

        var averageRating = await selectCommand.ExecuteScalarAsync();

        if (averageRating == null)
        {
            return 0;
        }

        return Convert.ToSingle(averageRating);
    }

    protected override void CreateTables()
    {
        var createTableQuery =
            "CREATE TABLE IF NOT EXISTS Feedbacks (CustomerId INT, FeedbackId INTEGER PRIMARY KEY AUTOINCREMENT, ProductId INT, Rating INT, Message TEXT, FeedbackDate TEXT)";
        using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
        createTableCommand.ExecuteNonQuery();
    }
}
