using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public async Task<IUser> GetUserByIdAsync(int id)
        {
            IUser user = new User();
            string selectQuery = "SELECT * FROM Users WHERE Id = @id";

            await using var selectCommand = new SqliteCommand(selectQuery, Connection);
            selectCommand.Parameters.AddWithValue("@id", id);

            await using var reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                user.Id = reader.GetInt32(0);
                user.Username = reader.GetString(1);
                user.Password = reader.GetString(2);
                user.Email = reader.GetString(3);
            }

            return user;
        }

        public async Task AddUserAsync(IUser user)
        {
            string insertQuery = "INSERT INTO Users (Id, Username, Password, Email, Role) VALUES (@userId, @username, @password, @email, @role)";

            await using var insertCommand = new SqliteCommand(insertQuery, Connection);
            insertCommand.Parameters.AddWithValue("@userId", user.Id);
            insertCommand.Parameters.AddWithValue("@username", user.Username);
            insertCommand.Parameters.AddWithValue("@password", user.Password);
            insertCommand.Parameters.AddWithValue("@email", user.Email);
            insertCommand.Parameters.AddWithValue("@role", user.Role);

            await insertCommand.ExecuteNonQueryAsync();
        }

        public async Task<List<IUser>> GetUsersAsync()
        {
            List<IUser> users = new List<IUser>();
            string selectQuery = "SELECT * FROM Users";

            await using var selectCommand = new SqliteCommand(selectQuery, Connection);
            
            await using var reader = await selectCommand.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                IUser user = new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2),
                    Email = reader.GetString(3),
                };
                users.Add(user);
            }

            return users;
        }

        public async Task UpdateUserAsync(IUser user)
        {
            string updateQuery = "UPDATE Users SET Username = @username, Password = @password, Email = @email WHERE Id = @userId";
            
            await using var updateCommand = new SqliteCommand(updateQuery, Connection);
            updateCommand.Parameters.AddWithValue("@username", user.Username);
            updateCommand.Parameters.AddWithValue("@password", user.Password);
            updateCommand.Parameters.AddWithValue("@email", user.Email);
            updateCommand.Parameters.AddWithValue("@userId", user.Id);

            await updateCommand.ExecuteNonQueryAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            string deleteQuery = "DELETE FROM Users WHERE Id = @userId";
            
            await using var deleteCommand = new SqliteCommand(deleteQuery, Connection);
            deleteCommand.Parameters.AddWithValue("@userId", id);
            await deleteCommand.ExecuteNonQueryAsync();
        }

        protected override void CreateTables()
        {
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INT PRIMARY KEY, Username TEXT, Password TEXT, Email TEXT)";
            using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
            createTableCommand.ExecuteNonQuery();
        }
    }
}
