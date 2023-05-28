namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class Feedback
{
    public Feedback(Customer customer, string message)
    {
        Customer = customer;
        Message = message;
        FeedbackDate = DateTime.Now;
    }

    public Feedback(int id, Customer customer, string message, DateTime feedbackDate)
    {
        Id = id;
        Customer = customer;
        Message = message;
        FeedbackDate = feedbackDate;
    }

    public int Id { get; set; }
    public Customer Customer { get; set; }
    public string Message { get; set; }
    public DateTime FeedbackDate { get; set; }
}