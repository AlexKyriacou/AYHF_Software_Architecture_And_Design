namespace MyProject.Domain.Interfaces;

public interface IPayment
{
    void ChargePayment(decimal amount, string customerName, string address, string paymentDetails);
    void RefundPayment(decimal amount, string customerName, string paymentDetails);
}