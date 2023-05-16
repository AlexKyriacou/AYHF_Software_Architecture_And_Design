using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace MyProject.Domain.Models;

public class Customer : IUser
{
    // Additional properties specific to customers
    public List<Order> Orders { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public void Save()
    {
        // Logic to save the customer to the database
    }

    public void Update()
    {
        // Logic to update the customer in the database
    }

    public void Delete()
    {
        // Logic to delete the customer from the database
    }

    // Additional methods specific to customers
}