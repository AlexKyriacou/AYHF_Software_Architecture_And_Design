using Microsoft.Data.Sqlite;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;

/// <summary>
/// Repository class for common database methods.
/// </summary>
/// <remarks>
/// All concrete repository classes should inherit from this class.
/// </remarks>
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

    /// <summary>
    /// Initializes a new instance of the RepositoryBase class.
    /// </summary>
    /// <remarks>
    /// If the database doesn't exist, it is created. Otherwise, creates the necessary tables.
    /// </remarks>
    protected RepositoryBase()
    {
        if (!DatabaseExists(_databasePath)) CreateDatabase(_databasePath);
        CreateTables();
    }

    /// <summary>
    /// Gets the connection instance.
    /// </summary>
    /// <remarks>
    /// The connection instance is created lazily if it hasn't been created yet.
    /// </remarks>
    protected SqliteConnection Connection => _lazyConnection.Value;

    /// <summary>
    /// Creates the necessary tables for the repository if they don't exist.
    /// </summary>
    protected abstract void CreateTables();

    /// <summary>
    /// Disposes of the connection instance.
    /// </summary>
    public void Dispose()
    {
        Connection.Close();
        Connection.Dispose();
    }

    /// <summary>
    /// Checks if the database file exists.
    /// </summary>
    /// <param name="databasePath">The path to the database file.</param>
    /// <returns>True if the file exists.</returns>
    private bool DatabaseExists(string databasePath)
    {
        return File.Exists(databasePath);
    }

    /// <summary>
    /// Creates the database file if it doesn't exist.
    /// </summary>
    /// <param name="databasePath">The path to the database file.</param>
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