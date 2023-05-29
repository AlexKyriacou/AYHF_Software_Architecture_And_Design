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
        var insertQuery =
            "INSERT INTO Feedbacks (UserId, ProductId, Rating, Message, FeedbackDate) VALUES (@userId, @productId, @rating, @message, @feedbackDate);";
        var updateQuery =
            "UPDATE Products SET NumRatings = NumRatings + 1, AvgRating = ((AvgRating*NumRatings)+@rating)/NumRatings WHERE Id = @productId;";
        var autoIncrement = "SELECT last_insert_rowid();";

        await using var command = new SqliteCommand(insertQuery + updateQuery + autoIncrement, Connection);
        command.Parameters.AddWithValue("@userId", feedback.UserId);
        command.Parameters.AddWithValue("@productId", feedback.ProductId);
        command.Parameters.AddWithValue("@rating", feedback.Rating);
        command.Parameters.AddWithValue("@message", feedback.Message);
        command.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);

        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt32(result);
    }


    public async Task UpdateFeedbackAsync(Feedback feedback)
    {
        var updateQuery =
            "UPDATE Feedbacks SET UserId = @userId, ProductId = @productId, Rating = @rating, Message = @message, FeedbackDate = @feedbackDate WHERE FeedbackId = @feedbackId";

        await using var updateCommand = new SqliteCommand(updateQuery, Connection);
        updateCommand.Parameters.AddWithValue("@userId", feedback.UserId);
        updateCommand.Parameters.AddWithValue("@productId", feedback.ProductId);
        updateCommand.Parameters.AddWithValue("@rating", feedback.Rating);
        updateCommand.Parameters.AddWithValue("@message", feedback.Message);
        updateCommand.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);
        updateCommand.Parameters.AddWithValue("@feedbackId", feedback.Id);

        await updateCommand.ExecuteNonQueryAsync();
    }

    public async Task DeleteFeedbackAsync(Feedback feedback)
    {
        await DeleteFeedbackAsync(feedback.Id);
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
        var selectQuery =
            "SELECT UserId, ProductId, Rating, Message, FeedbackDate FROM Feedbacks WHERE FeedbackId = @feedbackId";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@feedbackId", feedbackId);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            var userId = reader.GetInt32(0);
            var productId = reader.GetInt32(1);
            var rating = reader.GetInt32(2);
            var message = reader.GetString(3);
            var feedbackDate = reader.GetString(4);
            var customerRepository = new UserRepository();
            var customer = await customerRepository.GetUserByIdAsync(userId);
            if (customer != null)
            {
                // Create a new Feedback object using the retrieved data
                var feedback = new Feedback(feedbackId, customer.Id, productId, rating, message,
                    DateTime.Parse(feedbackDate));
                return feedback;
            }
        }

        return null; // Feedback not found
    }

    public async Task<List<Feedback>> GetAllFeedbackAsync()
    {
        var feedbacks = new List<Feedback>();
        var sql =
            "SELECT FeedbackId, UserId, ProductId, Rating, Message, FeedbackDate FROM Feedbacks ORDER BY FeedbackDate DESC";

        await using var command = new SqliteCommand(sql, Connection);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var feedbackId = reader.GetInt32(0);
            var userId = reader.GetInt32(1);
            var productId = reader.GetInt32(2);
            var rating = reader.GetInt32(3);
            var message = reader.GetString(4);
            var feedbackDateString = reader.GetString(5);
            var customerRepository = new UserRepository();
            var customer = await customerRepository.GetUserByIdAsync(userId);
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
        var sql =
            "SELECT FeedbackId, UserId, Rating, Message, FeedbackDate FROM Feedbacks WHERE ProductId = @productId ORDER BY FeedbackDate DESC";

        await using var command = new SqliteCommand(sql, Connection);
        command.Parameters.AddWithValue("@productId", productId);

        await using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var feedbackId = reader.GetInt32(0);
            var userId = reader.GetInt32(1);
            var rating = reader.GetInt32(2);
            var message = reader.GetString(3);
            var feedbackDateString = reader.GetString(4);
            var customerRepository = new UserRepository();
            var customer = await customerRepository.GetUserByIdAsync(userId);
            if (customer != null)
            {
                var feedback = new Feedback(feedbackId, customer.Id, productId, rating, message,
                    DateTime.Parse(feedbackDateString));
                feedbacks.Add(feedback);
            }
        }

        return feedbacks;
    }

    protected override void CreateTables()
    {
        var createTableQuery =
            "CREATE TABLE IF NOT EXISTS Feedbacks (UserId INT, FeedbackId INTEGER PRIMARY KEY AUTOINCREMENT, ProductId INT, Rating INT, Message TEXT, FeedbackDate TEXT)";
        using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
        createTableCommand.ExecuteNonQuery();
    }
}