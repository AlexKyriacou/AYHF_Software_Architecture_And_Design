using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Application.Services
{
    public class FeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository FeedbackRepository)
        {
            _feedbackRepository = FeedbackRepository;
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int FeedbackId)
        {
            return await Task.Run(() => _feedbackRepository.GetFeedbackByIdAsync(FeedbackId));
        }

        public async Task<List<Feedback>> GetAllFeedbacksAsync()
        {
            return await Task.Run(() => _feedbackRepository.GetAllFeedbackAsync());
        }

        public async Task AddFeedbackAsync(Feedback Feedback)
        {
            await Task.Run(() => _feedbackRepository.AddFeedbackAsync(Feedback));
        }

        public async Task UpdateFeedbackAsync(Feedback Feedback)
        {
            await Task.Run(() => _feedbackRepository.UpdateFeedbackAsync(Feedback));
        }

        public async Task DeleteFeedbackAsync(Feedback Feedback)
        {
            await Task.Run(() => _feedbackRepository.DeleteFeedbackAsync(Feedback));
        }
    }
}
