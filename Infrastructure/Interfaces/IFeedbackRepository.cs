using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories
{
    public interface IFeedbackRepository
    {
        Task AddFeedbackAsync(Feedback feedback);
        Task UpdateFeedbackAsync(Feedback feedback);
        Task DeleteFeedbackAsync(Feedback feedback);
        Task<Feedback?> GetFeedbackByIdAsync(int feedbackId);
        Task<List<Feedback>> GetAllFeedbackAsync();
    }
}