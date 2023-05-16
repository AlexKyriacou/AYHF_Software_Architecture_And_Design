using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class FeedbackRepository
{
    private readonly List<Feedback> _feedbackList;

    public FeedbackRepository()
    {
        _feedbackList = new List<Feedback>();
    }

    public void AddFeedback(Feedback feedback)
    {
        _feedbackList.Add(feedback);
    }

    public void UpdateFeedback(Feedback feedback)
    {
        // Update the feedback in the database
    }

    public void DeleteFeedback(Feedback feedback)
    {
        _feedbackList.Remove(feedback);
    }

    public Feedback GetFeedbackById(int feedbackId)
    {
        // Retrieve the feedback from the database by ID
        return _feedbackList.Find(f => f.Id == feedbackId);
    }

    public List<Feedback> GetAllFeedback()
    {
        // Retrieve all feedback from the database
        return _feedbackList;
    }
}