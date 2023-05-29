namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class Feedback
{
    public Feedback(int id, int userId, int rating, int productId, string message, DateTime feedbackDate)
    {
        Id = id;
        UserId = userId;
        Message = message;
        FeedbackDate = feedbackDate;
        Rating = rating;
        ProductId = productId;
    }

    public int Id { get; set; }
    public int UserId { get; set; }
    public int Rating { get; set; }
    public int ProductId { get; set; }
    public string Message { get; set; }
    public DateTime FeedbackDate { get; set; }
}