namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

/// <summary>
/// Represents an order entity in the system.
/// </summary>
public class Order
{
    /// <summary>
    /// Gets or sets the unique identifier for the order entity.
    /// </summary>
    /// <value>The unique identifier for the order entity.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the date the order was created.
    /// </summary>
    /// <value>The date the order was created.</value>
    public DateTime OrderDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the total amount of the order.
    /// </summary>
    /// <value>The total amount of the order.</value>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the order is completed.
    /// </summary>
    /// <value><c>true</c> if the order is completed; otherwise, <c>false</c>.</value>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// Gets or sets the customer associated with the order.
    /// </summary>
    /// <value>The customer associated with the order.</value>
    public Customer Customer { get; set; }

    /// <summary>
    /// Gets or sets the user identifier for the order.
    /// </summary>
    /// <value>The integer identifier for the user.</value>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the products associated with the order.
    /// </summary>
    /// <value>The products associated with the order.</value>
    public List<Product> Products { get; set; } = new();

    /// <summary>
    /// Calculates the total amount of the order based on its products.
    /// </summary>
    public void CalculateTotalAmount()
    {
        TotalAmount = 0;
        foreach (var product in Products) TotalAmount += product.Price;
    }

    /// <summary>
    /// Marks the order as completed.
    /// </summary>
    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}
