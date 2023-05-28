using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
    public UserRepository(): base()
    {
        CreateTables();
    }

    public async Task<IUser?> GetUserByIdAsync(int id)
    {
        IUser? user = null;
        var selectQuery = "SELECT * FROM Users WHERE Id = @id";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);
        selectCommand.Parameters.AddWithValue("@id", id);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        if (await reader.ReadAsync())
        {
            user = new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Username = reader.GetString(2),
                Email = reader.GetString(3),
                Password = reader.GetString(4),
                Role = reader.GetString(5)
            };
        }

        return user;
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
            user = new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Username = reader.GetString(2),
                Email = reader.GetString(3),
                Password = reader.GetString(4),
                Role = reader.GetString(5)
            };
        }

        return user;
    }


    public async Task AddUserAsync(IUser user)
    {
        var insertQuery =
            "INSERT INTO Users (Name, Username, Email, Password, Role) VALUES (@name, @username, @email, @password, @role)";
        await using var insertCommand = new SqliteCommand(insertQuery, Connection);
        insertCommand.Parameters.AddWithValue("@name", user.Name);
        insertCommand.Parameters.AddWithValue("@username", user.Username);
        insertCommand.Parameters.AddWithValue("@password", user.Password);
        insertCommand.Parameters.AddWithValue("@email", user.Email);
        insertCommand.Parameters.AddWithValue("@role", user.Role);

        await insertCommand.ExecuteNonQueryAsync();
    }

    public async Task<List<IUser>> GetUsersAsync()
    {
        var users = new List<IUser>();
        var selectQuery = "SELECT * FROM Users";

        await using var selectCommand = new SqliteCommand(selectQuery, Connection);

        await using var reader = await selectCommand.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            IUser user = new User
            {
                Id = !reader.IsDBNull(0) ? reader.GetInt32(0) : 0,
                Name = !reader.IsDBNull(1) ? reader.GetString(1) : string.Empty,
                Username = !reader.IsDBNull(2) ? reader.GetString(2) : string.Empty,
                Email = !reader.IsDBNull(4) ? reader.GetString(3) : string.Empty,
                Password = !reader.IsDBNull(3) ? reader.GetString(4) : string.Empty,
                Role = !reader.IsDBNull(5) ? reader.GetString(5) : string.Empty
            };
            users.Add(user);
        }

        return users;
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

    protected override void CreateTables()
    {
        var createTableQuery =
            "CREATE TABLE IF NOT EXISTS Users (Id INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT, Username TEXT, Email TEXT, Password TEXT, Role TEXT)";
        using var createTableCommand = new SqliteCommand(createTableQuery, Connection);
        createTableCommand.ExecuteNonQuery();
    }
}