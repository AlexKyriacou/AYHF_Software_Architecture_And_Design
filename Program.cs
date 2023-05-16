using System;
using System.Data.SQLite;

class Program
{
    static void Main()
    {
        // Set the path to your SQLite database file
        string databasePath = "C:\\Uni\\Software Architecture & Design\\AYHF\\AYHF_Software_Architecture_And_Design\\src\\database.db";

        // Create a SQLite connection
        using (SQLiteConnection connection = CreateSQLiteConnection(databasePath))
        {
            // Open the connection
            connection.Open();


            // Close the connection
            connection.Close();
        }
    }

    static SQLiteConnection CreateSQLiteConnection(string databasePath)
    {
        string connectionString = $"Data Source={databasePath};Version=3;";
        SQLiteConnection connection = new SQLiteConnection(connectionString);
        return connection;
    }
}