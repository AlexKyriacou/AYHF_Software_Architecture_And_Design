namespace MyProject.Domain.Models;

public class Order
{
    public Order(Customer customer, List<Product> products)
    {
        Customer = customer;
        Products = products;
        OrderDate = DateTime.Now;
        IsCompleted = false;

        CalculateTotalAmount();
    }

    public int Id { get; set; }
    public Customer Customer { get; set; }
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