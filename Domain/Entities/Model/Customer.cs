using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

/// <summary>
/// Represents a customer entity that implements the IUser interface.
/// </summary>
public class Customer : IUser
{
    /// <summary>
    /// Gets or sets the list of orders placed by the customer.
    /// </summary>
    /// <value>The list of orders placed by the customer.</value>
    public List<Order> Orders { get; set; }

    /// <summary>
    /// Gets or sets the delivery address of the customer.
    /// </summary>
    /// <value>The delivery address of the customer.</value>
    public DeliveryAddress DeliveryAddress { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the customer.
    /// </summary>
    /// <value>The unique identifier for the customer.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the customer.
    /// </summary>
    /// <value>The name of the customer.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the username of the customer.
    /// </summary>
    /// <value>The username of the customer.</value>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the customer.
    /// </summary>
    /// <value>The password of the customer.</value>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the email of the customer.
    /// </summary>
    /// <value>The email of the customer.</value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the role of the customer.
    /// </summary>
    /// <value>The role of the customer.</value>
    public UserRole Role { get; set; }
}
