namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

/// <summary>
/// Class representing a feedback data transfer object.
/// </summary>
public class FeedbackDto
{
    /// <summary>
    /// Gets or sets the ID for the feedback.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who provided the feedback.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the rating the user gave.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the ID of the product which is being rated.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the feedback message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the date of the feedback.
    /// </summary>
    public DateTime FeedbackDate { get; set; }
}
