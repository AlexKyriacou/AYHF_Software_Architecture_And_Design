using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace MyProject.Domain.Models;

public class User : IUser
{
    public User()
    {
        Role = "User";
    }

    public string Role { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public void Save()
    {
        Console.WriteLine("Saving user...");
        // Save user logic
    }

    public void Update()
    {
        Console.WriteLine("Updating user...");
        // Update user logic
    }

    public void Delete()
    {
        Console.WriteLine("Deleting user...");
        // Delete user logic
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"Sending message: {message}");
        // Send message logic
    }
}