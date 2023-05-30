using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

/// <summary>
/// Represents an interface for managing feedback entities in the system.
/// </summary>
public interface IFeedbackRepository
{
    /// <summary>
    /// Adds a new feedback entity to the system.
    /// </summary>
    /// <param name="feedback">The feedback entity to be added.</param>
    /// <returns>An integer representing the result of the operation.</returns>
    Task<int> AddFeedbackAsync(Feedback feedback);

    /// <summary>
    /// Updates an existing feedback entity in the system.
    /// </summary>
    /// <param name="feedback">The feedback entity to be updated.</param>
    /// <returns>An integer representing the result of the operation.</returns>
    Task UpdateFeedbackAsync(Feedback feedback);

    /// <summary>
    /// Deletes an existing feedback entity from the system using the entity itself.
    /// </summary>
    /// <param name="feedback">The feedback entity to be deleted.</param>
    /// <returns>An integer representing the result of the operation.</returns>
    Task DeleteFeedbackAsync(Feedback feedback);

    /// <summary>
    /// Deletes an existing feedback entity from the system using the identifier.
    /// </summary>
    /// <param name="id">The integer identifier of the feedback entity to be deleted.</param>
    /// <returns>An integer representing the result of the operation.</returns>
    Task DeleteFeedbackAsync(int id);

    /// <summary>
    /// Gets an existing feedback entity from the system using the identifier.
    /// </summary>
    /// <param name="feedbackId">The integer identifier of the feedback entity to be retrieved.</param>
    /// <returns>A nullable feedback entity.</returns>
    Task<Feedback?> GetFeedbackByIdAsync(int feedbackId);

    /// <summary>
    /// Gets all feedback entities in the system.
    /// </summary>
    /// <returns>A list of feedback entities.</returns>
    Task<List<Feedback>> GetAllFeedbackAsync();

    /// <summary>
    /// Gets all feedback entities in the system associated with a specific product.
    /// </summary>
    /// <param name="productId">The integer identifier of the product associated with the feedback entities.</param>
    /// <returns>A list of feedback entities associated with the product.</returns>
    Task<List<Feedback>> GetAllFeedbackForProductAsync(int productId);
}
