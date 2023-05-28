using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Repositories;

namespace MyProject.Domain.Models;

public class Admin : IUser
{
    // Additional properties specific to admins
    public List<IReport> Reports { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }

}