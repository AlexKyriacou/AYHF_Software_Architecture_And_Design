using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

/// <summary>
/// Data transfer object representing Order.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Integer property representing primary key of the order.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Integer property representing user id associated with the order.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// List of Products associated with the order.
    /// </summary>
    /// <value>A list of product objects</value>
    public List<Product> Products { get; set; }
}
