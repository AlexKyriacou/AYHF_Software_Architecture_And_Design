using MyProject.Domain.Interfaces;

namespace MyProject.Domain.Models;

public class CreditCardPayment : IPayment
{
    public void ChargePayment(decimal amount, string customerName, string address, string paymentDetails)
    {
        // Credit card-specific logic to charge the payment
        Console.WriteLine(
            $"Charging {amount} USD via Credit Card for customer {customerName} at address {address}. Payment details: {paymentDetails}");
    }

    public void RefundPayment(decimal amount, string customerName, string paymentDetails)
    {
        // Credit card-specific logic to refund the payment
        Console.WriteLine(
            $"Refunding {amount} USD via Credit Card for customer {customerName}. Payment details: {paymentDetails}");
    }
}