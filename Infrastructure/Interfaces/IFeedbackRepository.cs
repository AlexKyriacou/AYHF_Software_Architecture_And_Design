using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

public interface IFeedbackRepository
{
    Task AddFeedbackAsync(Feedback feedback);
    Task UpdateFeedbackAsync(Feedback feedback);
    Task DeleteFeedbackAsync(Feedback feedback);
    Task DeleteFeedbackAsync(int id);
    Task<Feedback?> GetFeedbackByIdAsync(int feedbackId);
    Task<List<Feedback>> GetAllFeedbackAsync();
}