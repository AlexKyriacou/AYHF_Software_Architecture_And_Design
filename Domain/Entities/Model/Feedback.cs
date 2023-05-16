namespace MyProject.Domain.Models;

public class Feedback
{
    public Feedback(Customer customer, string message)
    {
        Customer = customer;
        Message = message;
        FeedbackDate = DateTime.Now;
    }

    public int Id { get; set; }
    public Customer Customer { get; set; }
    public string Message { get; set; }
    public DateTime FeedbackDate { get; set; }
}