namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

/// <summary>
/// Represents a delivery address entity that describes a physical location for delivery.
/// </summary>
public class DeliveryAddress
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeliveryAddress"/> class.
    /// </summary>
    /// <param name="street">The street of the address.</param>
    /// <param name="city">The city of the address.</param>
    /// <param name="state">The state of the address.</param>
    /// <param name="postalCode">The postal code of the address.</param>
    public DeliveryAddress(string street, string city, string state, string postalCode)
    {
        Street = street;
        City = city;
        State = state;
        PostalCode = postalCode;
    }

    /// <summary>
    /// Gets or sets the street of the address.
    /// </summary>
    /// <value>The street of the address.</value>
    public string Street { get; set; }

    /// <summary>
    /// Gets or sets the city of the address.
    /// </summary>
    /// <value>The city of the address.</value>
    public string City { get; set; }

    /// <summary>
    /// Gets or sets the state of the address.
    /// </summary>
    /// <value>The state of the address.</value>
    public string State { get; set; }

    /// <summary>
    /// Gets or sets the postal code of the address.
    /// </summary>
    /// <value>The postal code of the address.</value>
    public string PostalCode { get; set; }

    /// <summary>
    /// Gets or sets the customer who the address belongs to.
    /// </summary>
    /// <value>The customer who the address belongs to.</value>
    public Customer Customer { get; set; }
}
