using Microsoft.Data.Sqlite;
using MyProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace MyProject.Infrastructure.Repositories
{
    public class FeedbackRepository : RepositoryBase
    {
        private static FeedbackRepository? _instance;

        private FeedbackRepository() : base()
        {
            _instance = this;
        }

        public static FeedbackRepository Instance
        {
            get
            {
                _instance ??= new FeedbackRepository();
                return _instance;
            }
        }

        protected override void CreateTables()
        {
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Feedbacks (CustomerId INT, FeedbackId INT PRIMARY KEY, Message TEXT, FeedbackDate TEXT)";
            using (var createTableCommand = new SqliteCommand(createTableQuery, Connection))
            {
                createTableCommand.ExecuteNonQuery();
            }
        }

        public void AddFeedback(Feedback feedback)
        {
            CreateTables();

            using (var transaction = Connection.BeginTransaction())
            {
                var command = Connection.CreateCommand();
                command.CommandText = "INSERT INTO Feedbacks (CustomerId, FeedbackId, Message, FeedbackDate) VALUES (@customerId, @feedbackId, @message, @feedbackDate)";
                command.Parameters.AddWithValue("@customerId", feedback.Customer.Id);
                command.Parameters.AddWithValue("@feedbackId", feedback.Id);
                command.Parameters.AddWithValue("@message", feedback.Message);
                command.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);

                command.ExecuteNonQuery();

                transaction.Commit();
            }
        }

        public void UpdateFeedback(Feedback feedback)
        {
            string updateQuery = "UPDATE Feedbacks SET CustomerId = @customerId, Message = @message, FeedbackDate = @feedbackDate WHERE FeedbackId = @feedbackId";
            using (var updateCommand = new SqliteCommand(updateQuery, Connection))
            {
                updateCommand.Parameters.AddWithValue("@customerId", feedback.Customer.Id);
                updateCommand.Parameters.AddWithValue("@message", feedback.Message);
                updateCommand.Parameters.AddWithValue("@feedbackDate", feedback.FeedbackDate);
                updateCommand.Parameters.AddWithValue("@feedbackId", feedback.Id);

                updateCommand.ExecuteNonQuery();
            }
        }

        public void DeleteFeedback(Feedback feedback)
        {
            string deleteQuery = "DELETE FROM Feedbacks WHERE FeedbackId = @feedbackId";
            using (var deleteCommand = new SqliteCommand(deleteQuery, Connection))
            {
                deleteCommand.Parameters.AddWithValue("@feedbackId", feedback.Id);
                deleteCommand.ExecuteNonQuery();
            }
        }

        public Feedback? GetFeedbackById(int feedbackId)
        {
            string selectQuery = "SELECT CustomerId, Message, FeedbackDate FROM Feedbacks WHERE FeedbackId = @feedbackId";
            using (var selectCommand = new SqliteCommand(selectQuery, Connection))
            {
                selectCommand.Parameters.AddWithValue("@feedbackId", feedbackId);

                using (var reader = selectCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int customerId = reader.GetInt32(0);
                        string message = reader.GetString(1);
                        string feedbackDate = reader.GetString(2);
                        Customer? customer = UserRepository.Instance.GetUserById<Customer>(customerId);
                        if (customer != null)
                        {
                            // Create a new Feedback object using the retrieved data
                            Feedback feedback = new Feedback(feedbackId, customer, message, DateTime.Parse(feedbackDate));
                            return feedback;
                        }
                    }

                }
            }

            return null; // Feedback not found
        }


        public List<Feedback> GetAllFeedback()
        {
            List<Feedback> feedbacks = new List<Feedback>();
            string sql = "SELECT FeedbackId, CustomerId, Message, FeedbackDate FROM Feedbacks ORDER BY FeedbackDate DESC";

            using (var command = new SqliteCommand(sql, Connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int feedbackId = reader.GetInt32(0);
                        int customerId = reader.GetInt32(1);
                        string message = reader.GetString(2);
                        string feedbackDateString = reader.GetString(3);
                        Customer? customer = UserRepository.Instance.GetUserById<Customer>(customerId);
                        if (customer != null)
                        {
                            Feedback feedback = new Feedback(feedbackId, customer, message, DateTime.Parse(feedbackDateString));
                            feedbacks.Add(feedback);
                        }
                    }
                }
            }

            return feedbacks;
        }
    }
}
