namespace MyProject.Domain.Models;

public class DeliveryAddress
{
    // Constructor
    public DeliveryAddress(string street, string city, string state, string postalCode)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }

    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }

    public string PostalCode { get; set; }
    // Additional address properties

    public Customer Customer { get; set; }

    // Additional methods
}