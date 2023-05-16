using MyProject.Domain.Interfaces;

namespace MyProject.Domain.Models;

public class PayPalPayment : IPayment
{
    public void ChargePayment(decimal amount, string customerName, string address, string paymentDetails)
    {
        // PayPal-specific logic to charge the payment
        Console.WriteLine(
            $"Charging {amount} USD via PayPal for customer {customerName} at address {address}. Payment details: {paymentDetails}");
    }

    public void RefundPayment(decimal amount, string customerName, string paymentDetails)
    {
        // PayPal-specific logic to refund the payment
        Console.WriteLine(
            $"Refunding {amount} USD via PayPal for customer {customerName}. Payment details: {paymentDetails}");
    }
}