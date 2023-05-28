namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class Order
{
    public Order(int customerId, List<Product> products)
    {
        CustomerId = customerId;
        Products = products;
        OrderDate = DateTime.Now;
        IsCompleted = false;

        CalculateTotalAmount();
    }

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<Product> Products { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public bool IsCompleted { get; set; }

    private void CalculateTotalAmount()
    {
        TotalAmount = 0;
        foreach (var product in Products) TotalAmount += product.Price;
    }

    public void MarkAsCompleted()
    {
        IsCompleted = true;
    }
}