namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class Feedback
{
    public Feedback(int customerId, string message)
    {
        CustomerId = customerId;
        Message = message;
        FeedbackDate = DateTime.Now;
    }

    public Feedback(int id, int customerId, string message, DateTime feedbackDate)
    {
        Id = id;
        CustomerId = customerId;
        Message = message;
        FeedbackDate = feedbackDate;
    }

    public int Id { get; set; }
    public int CustomerId { get; set; }
    public string Message { get; set; }
    public DateTime FeedbackDate { get; set; }
}