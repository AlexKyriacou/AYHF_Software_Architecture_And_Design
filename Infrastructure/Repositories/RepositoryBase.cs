using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;

public abstract class RepositoryBase
{
    private static readonly string _databasePath = "Infrastructure\\Data\\AYHFDatabase.db";

    private static readonly Lazy<SqliteConnection> _lazyConnection = new(() =>
    {
        var connectionString = $"Data Source={_databasePath};";
        var connection = new SqliteConnection(connectionString);
        connection.Open();
        return connection;
    });

    protected RepositoryBase()
    {
        if (!DatabaseExists(_databasePath)) CreateDatabase(_databasePath);
        CreateTables();
    }

    protected SqliteConnection Connection => _lazyConnection.Value;

    protected abstract void CreateTables();

    public void Dispose()
    {
        Connection.Close();
        Connection.Dispose();
    }

    private bool DatabaseExists(string databasePath)
    {
        return File.Exists(databasePath);
    }

    private void CreateDatabase(string databasePath)
    {
        try
        {
            using (File.Create(databasePath))
            {
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred during database creation: {ex.Message}");
        }
    }
}