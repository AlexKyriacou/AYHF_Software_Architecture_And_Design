using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using MyProject.Domain.Interfaces;

namespace MyProject.Domain.Models;

public class Admin : IUser
{
    // Additional properties specific to admins
    public List<IReport> Reports { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public void Save()
    {
        // Logic to save the admin to the database
    }

    public void Update()
    {
        // Logic to update the admin in the database
    }

    public void Delete()
    {
        // Logic to delete the admin from the database
    }

    // Additional methods specific to admins
}