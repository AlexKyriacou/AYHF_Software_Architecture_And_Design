using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class Customer : IUser
{
    // Additional properties specific to customers
    public List<Order> Orders { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}