using MyProject.Domain.Interfaces;
using MyProject.Infrastructure.Repositories;

namespace MyProject.Domain.Models;

public class FeedbackReport : IReport
{
    private readonly FeedbackRepository _feedbackRepository;

    public FeedbackReport(FeedbackRepository feedbackRepository)
    {
        _feedbackRepository = feedbackRepository;
    }

    public void GenerateReport()
    {
        // Logic to generate feedback report using the FeedbackRepository
        Console.WriteLine("Generating feedback report...");

        // Retrieve feedback from the repository and process it for reporting
        var feedbackList = _feedbackRepository.GetAllFeedback();

        // Perform necessary calculations and generate the report
        // ...

        Console.WriteLine("Feedback report generated successfully.");
    }
}