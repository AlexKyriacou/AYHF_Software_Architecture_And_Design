using Microsoft.Data.Sqlite;
using System;

namespace MyProject.Infrastructure.Repositories
{
    /// <summary>
    /// Base abstract class for repositories that encapsulates connection details and singleton implementation.
    /// </summary>
    public abstract class RepositoryBase
    {
        private static string _databasePath = "AYHFDatabase.db";

        private static readonly Lazy<SqliteConnection> _lazyConnection = new Lazy<SqliteConnection>(() =>
        {
            string connectionString = $"Data Source={_databasePath};";
            var connection = new SqliteConnection(connectionString);
            connection.Open();
            return connection;
        });

        /// <summary>
        /// The singleton instance of the SQLite connection.
        /// </summary>
        protected SqliteConnection Connection => _lazyConnection.Value;

        /// <summary>
        /// Initializes a new instance of the RepositoryBase class.
        /// </summary>
        protected RepositoryBase()
        {
            if (!DatabaseExists(_databasePath))
            {
                CreateDatabase(_databasePath);
            }
            CreateTables();
        }

        /// <summary>
        /// Creates the necessary tables for the repository.
        /// This method should be implemented by derived repository classes.
        /// </summary>
        protected abstract void CreateTables();

        /// <summary>
        /// Disposes the repository by closing and disposing the connection.
        /// </summary>
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
            File.Create(databasePath);
        }
    }
}
