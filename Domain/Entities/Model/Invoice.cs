namespace MyProject.Domain.Models;

public class Invoice
{
    public Invoice(decimal totalAmount, string paymentMethod, string billingAddress)
    {
        TotalAmount = totalAmount;
        PaymentMethod = paymentMethod;
        BillingAddress = billingAddress;
        BillingDate = DateTime.Now;
    }

    public decimal TotalAmount { get; set; }
    public string PaymentMethod { get; set; }
    public string BillingAddress { get; set; }
    public DateTime BillingDate { get; set; }
}