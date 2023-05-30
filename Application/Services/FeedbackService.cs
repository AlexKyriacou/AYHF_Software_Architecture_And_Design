using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

/// <summary>
/// Represents a service for managing feedback.
/// </summary>
public class FeedbackService
{
    private readonly IFeedbackRepository _feedbackRepository;

    /// <summary>
    /// Constructor for FeedbackService class.
    /// </summary>
    /// <param name="FeedbackRepository">An interface for feedback repositories.</param>
    public FeedbackService(IFeedbackRepository FeedbackRepository)
    {
        _feedbackRepository = FeedbackRepository;
    }

    /// <summary>
    /// Gets a Feedback object by its id asynchronously.
    /// </summary>
    /// <param name="FeedbackId">A feedback's id.</param>
    /// <returns>A feedback object.</returns>
    public async Task<Feedback?> GetFeedbackByIdAsync(int FeedbackId)
    {
        return await Task.Run(() => _feedbackRepository.GetFeedbackByIdAsync(FeedbackId));
    }

    /// <summary>
    /// Gets all feedback asynchronously.
    /// </summary>
    /// <returns>A list of feedback objects.</returns>
    public async Task<List<Feedback>> GetAllFeedbacksAsync()
    {
        return await Task.Run(() => _feedbackRepository.GetAllFeedbackAsync());
    }

    /// <summary>
    /// Adds a feedback asynchronously.
    /// </summary>
    /// <param name="Feedback">A feedback object.</param>
    /// <returns>The id of the feedback object added.</returns>
    public async Task<int> AddFeedbackAsync(Feedback Feedback)
    {
        return await Task.Run(() => _feedbackRepository.AddFeedbackAsync(Feedback));
    }

    /// <summary>
    /// Updates a feedback asynchronously.
    /// </summary>
    /// <param name="Feedback">A feedback object.</param>
    public async Task UpdateFeedbackAsync(Feedback Feedback)
    {
        await Task.Run(() => _feedbackRepository.UpdateFeedbackAsync(Feedback));
    }

    /// <summary>
    /// Deletes a feedback by its id asynchronously.
    /// </summary>
    /// <param name="id">A feedback's id.</param>
    public async Task DeleteFeedbackAsync(int id)
    {
        await Task.Run(() => _feedbackRepository.DeleteFeedbackAsync(id));
    }

    /// <summary>
    /// Gets all feedbacks for a product by id asynchronously.
    /// </summary>
    /// <param name="id">A product's id.</param>
    /// <returns>A list of feedback objects.</returns>
    public async Task<List<Feedback>> GetAllFeedbackForProductAsync(int id)
    {
        return await Task.Run(() => _feedbackRepository.GetAllFeedbackForProductAsync(id));
    }

}
