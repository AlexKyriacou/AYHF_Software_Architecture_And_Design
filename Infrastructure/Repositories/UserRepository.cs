using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;
using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    private static UserRepository? _instance;

    private UserRepository()
    {
        _instance = this;
    }

    public static UserRepository Instance
    {
        get
        {
            _instance ??= new UserRepository();
            return _instance;
        }
    }

    public IUser GetUserById(int id)
    {
        IUser user = new User();
        string selectQuery = "SELECT * FROM Users WHERE Id = @id";
        using (var selectCommand = new SqliteCommand(selectQuery, Connection))
        {
            selectCommand.Parameters.AddWithValue("@id", id);
            using (var reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    user.Id = reader.GetInt32(0);
                    user.Username = reader.GetString(1);
                    user.Password = reader.GetString(2);
                    user.Email = reader.GetString(3);
                }
            }
        }
        return user;
    }

    public void AddUser(IUser user)
    {
        string insertQuery = "INSERT INTO Users (Id, Username, Password, Email, Role) VALUES (@userId, @username, @password, @email, @role)";
        using (var insertCommand = new SqliteCommand(insertQuery, Connection))
        {
            insertCommand.Parameters.AddWithValue("@userId", user.Id);
            insertCommand.Parameters.AddWithValue("@username", user.Username);
            insertCommand.Parameters.AddWithValue("@password", user.Password);
            insertCommand.Parameters.AddWithValue("@email", user.Email);

            insertCommand.ExecuteNonQuery();
        }
    }
    
    public List<IUser> GetUsers()
    {
        List<IUser> users = new List<IUser>();
        string selectQuery = "SELECT * FROM Users";
        using (var selectCommand = new SqliteCommand(selectQuery, Connection))
        {
            using (var reader = selectCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    IUser user = new User
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        Email = reader.GetString(3),
                        // role etc
                    };
                    users.Add(user);
                }
            }
        }

        return users;
    }
    
    public T? GetUserById<T>(int entityId) where T : IUser, new()
    {
        string selectQuery = "SELECT * FROM Users WHERE Id = @entityId";
        using (var selectCommand = new SqliteCommand(selectQuery, Connection))
        {
            selectCommand.Parameters.AddWithValue("@entityId", entityId);

            using (var reader = selectCommand.ExecuteReader())
            {
                if (reader.Read())
                {
                    T entity = new T
                    {
                        Id = reader.GetInt32(0),
                        Username = reader.GetString(1),
                        Password = reader.GetString(2),
                        Email = reader.GetString(3),
                    };

                    // Additional code specific to the entity type can be added here

                    return entity;
                }
            }
        }

        return default; // Return default value if the entity with the given ID is not found
    }


    public void UpdateUser(IUser user)
    {
        string updateQuery = "UPDATE Users SET Username = @username, Password = @password, Email = @email, Role = @role WHERE Id = @userId";
        using (var updateCommand = new SqliteCommand(updateQuery, Connection))
        {
            updateCommand.Parameters.AddWithValue("@username", user.Username);
            updateCommand.Parameters.AddWithValue("@password", user.Password);
            updateCommand.Parameters.AddWithValue("@email", user.Email);
            updateCommand.Parameters.AddWithValue("@userId", user.Id);

            updateCommand.ExecuteNonQuery();
        }
    }


    public void DeleteUser(IUser user)
    {
        string deleteQuery = "DELETE FROM Users WHERE Id = @userId";
        using (var deleteCommand = new SqliteCommand(deleteQuery, Connection))
        {
            deleteCommand.Parameters.AddWithValue("@userId", user.Id);
            deleteCommand.ExecuteNonQuery();
        }
    }

    protected override void CreateTables()
    {
        string createTableQuery = "CREATE TABLE IF NOT EXISTS Users (Id INT PRIMARY KEY, Username TEXT, Password TEXT, Email TEXT, Role TEXT)";
        using (var createTableCommand = new SqliteCommand(createTableQuery, Connection))
        {
            createTableCommand.ExecuteNonQuery();
        }
    }
}