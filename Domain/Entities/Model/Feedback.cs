namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

/// <summary>
/// Class representing feedback entity.
/// </summary>
public class Feedback
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Feedback"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the feedback entity.</param>
    /// <param name="userId">The unique identifier for the user who gave the feedback.</param>
    /// <param name="rating">The rating given by the user who gave the feedback.</param>
    /// <param name="productId">The unique identifier for the product entity which the feedback is about.</param>
    /// <param name="message">The message given by the user who gave the feedback.</param>
    /// <param name="feedbackDate">The date the feedback was given.</param>
    public Feedback(int id, int userId, int rating, int productId, string message, DateTime feedbackDate)
    {
        Id = id;
        UserId = userId;
        Message = message;
        FeedbackDate = feedbackDate;
        Rating = rating;
        ProductId = productId;
    }

    /// <summary>
    /// Gets or sets the unique identifier for the feedback entity.
    /// </summary>
    /// <value>The unique identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user who gave the feedback.
    /// </summary>
    /// <value>The unique identifier.</value>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the message given by the user who gave the feedback.
    /// </summary>
    /// <value>The message.</value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the date the feedback was given.
    /// </summary>
    /// <value>The date.</value>
    public DateTime FeedbackDate { get; set; }

    /// <summary>
    /// Gets or sets the rating given by the user who gave the feedback.
    /// </summary>
    /// <value>The rating.</value>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the product entity which the feedback is about.
    /// </summary>
    /// <value>The unique identifier.</value>
    public int ProductId { get; set; }
}
