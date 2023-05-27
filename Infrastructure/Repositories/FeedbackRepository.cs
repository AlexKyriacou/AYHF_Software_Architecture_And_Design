using Microsoft.Data.Sqlite;
using MyProject.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyProject.Infrastructure.Repositories
{
    public class FeedbackRepository : RepositoryBase, IFeedbackRepository
    {
        public FeedbackRepository() : base()
        {
            CreateTables();
        }

        protected override void CreateTables()
        {
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Feedbacks (CustomerId INT, FeedbackId INT PRIMARY KEY, Message TEXT, FeedbackDate TEXT)";
            using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
            createTableCommand.ExecuteNonQuery();
        }

        public async Task AddFeedbackAsync(Feedback feedback)
        {
            string insertQuery = "INSERT INTO Feedbacks (CustomerId, FeedbackId, Message, FeedbackDate) VALUES (@customerId, @feedbackId, @message, @feedbackDate)";

            await using var command = new SqliteCommand(insertQuery, Connection);
            command.Parameters.AddWithValue("@customerId", feedback.Customer.Id);
            command.Parameters.AddWithValue("@feedbackId", feedback.Id);
            command.Parameters.AddWithValue("@message", feedback.Message);
            command.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            string updateQuery = "UPDATE Feedbacks SET CustomerId = @customerId, Message = @message, FeedbackDate = @feedbackDate WHERE FeedbackId = @feedbackId";

            await using var updateCommand = new SqliteCommand(updateQuery, Connection);
            updateCommand.Parameters.AddWithValue("@customerId", feedback.Customer.Id);
            updateCommand.Parameters.AddWithValue("@message", feedback.Message);
            updateCommand.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);
            updateCommand.Parameters.AddWithValue("@feedbackId", feedback.Id);

            await updateCommand.ExecuteNonQueryAsync();
        }

        public async Task DeleteFeedbackAsync(Feedback feedback)
        {
            string deleteQuery = "DELETE FROM Feedbacks WHERE FeedbackId = @feedbackId";

            await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
            deleteCommand.Parameters.AddWithValue("@feedbackId", feedback.Id);
            await deleteCommand.ExecuteNonQueryAsync();
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int feedbackId)
        {
            string selectQuery = "SELECT CustomerId, Message, FeedbackDate FROM Feedbacks WHERE FeedbackId = @feedbackId";

            await using var selectCommand = new SqliteCommand(selectQuery, Connection);
            selectCommand.Parameters.AddWithValue("@feedbackId", feedbackId);

            await using var reader = await selectCommand.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                int customerId = reader.GetInt32(0);
                string message = reader.GetString(1);
                string feedbackDate = reader.GetString(2);
                var customerRepository = new UserRepository();
                var customer = await customerRepository.GetUserByIdAsync(customerId);
                if (customer != null)
                {
                    // Create a new Feedback object using the retrieved data
                    Feedback feedback = new Feedback(feedbackId, (Customer)customer, message, DateTime.Parse(feedbackDate));
                    return feedback;
                }
            }

            return null; // Feedback not found
        }

        public async Task<List<Feedback>> GetAllFeedbackAsync()
        {
            List<Feedback> feedbacks = new List<Feedback>();
            string sql = "SELECT FeedbackId, CustomerId, Message, FeedbackDate FROM Feedbacks ORDER BY FeedbackDate DESC";

            await using var command = new SqliteCommand(sql, Connection);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                int feedbackId = reader.GetInt32(0);
                int customerId = reader.GetInt32(1);
                string message = reader.GetString(2);
                string feedbackDateString = reader.GetString(3);
                var customerRepository = new UserRepository();
                var customer = await customerRepository.GetUserByIdAsync(customerId);
                if (customer != null)
                {
                    Feedback feedback = new Feedback(feedbackId, (Customer)customer, message, DateTime.Parse(feedbackDateString));
                    feedbacks.Add(feedback);
                }
            }

            return feedbacks;
        }
    }
}
