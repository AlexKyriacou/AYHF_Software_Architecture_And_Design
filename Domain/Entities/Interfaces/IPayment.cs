namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

public interface IPayment
{
    void ChargePayment(decimal amount, string customerName, string address, string paymentDetails);
    void RefundPayment(decimal amount, string customerName, string paymentDetails);
}