using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace MyProject.Domain.Models;

public class User : IUser
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } = "User";
}