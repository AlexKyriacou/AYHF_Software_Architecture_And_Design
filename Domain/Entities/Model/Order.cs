namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class Order
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public decimal TotalAmount { get; set; }
    public bool IsCompleted { get; set; }

    public Customer Customer { get; set; }
    public int UserId { get; set; }
    public List<Product> Products { get; set; } = new();

    public void CalculateTotalAmount()
    {
        TotalAmount = 0;
        foreach (var product in Products) TotalAmount += product.Price;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}