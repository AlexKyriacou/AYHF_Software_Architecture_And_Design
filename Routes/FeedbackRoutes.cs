using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AYHF_Software_Architecture_And_Design.Routes;

public class FeedbackRoutes
{
    private readonly WebApplication _app;

    public FeedbackRoutes(WebApplication app)
    {
        _app = app;
    }

    public void Configure()
    {
        _app.MapGet("/feedback/{id}", async (int id, [FromServices] FeedbackService feedbackService) =>
        {
            var feedback = await feedbackService.GetFeedbackByIdAsync(id);
            return feedback == null ? Results.NotFound() : Results.Ok(feedback);
        });

        _app.MapGet("/products/{id}/feedback", async (int id, [FromServices] FeedbackService feedbackService) =>
        {
            var feedback = await feedbackService.GetAllFeedbackForProductAsync(id);
            return Results.Ok(feedback);
        });

        _app.MapGet("/feedback",
            async ([FromServices] FeedbackService feedbackService) =>
            {
                return await feedbackService.GetAllFeedbacksAsync();
            });

        _app.MapPost("/feedback",
            async ([FromBody] FeedbackDto feedbackDto, [FromServices] FeedbackService feedbackService) =>
            {
                var userRepo = new UserRepository();
                var user = await userRepo.GetUserByIdAsync(feedbackDto.UserId);
                if (user == null) return Results.BadRequest();
                var feedback = new Feedback(feedbackDto.Id, feedbackDto.UserId, feedbackDto.Rating,
                    feedbackDto.ProductId, feedbackDto.Message, feedbackDto.FeedbackDate);
                var createdFeedbackId = await feedbackService.AddFeedbackAsync(feedback);
                if (createdFeedbackId <= 0) return Results.BadRequest(); // Handle the case if Id is not returned or invalid

                return Results.Created($"/feedback/{createdFeedbackId}", feedback);
            });

        _app.MapPut("/feedback",
            async ([FromBody] FeedbackDto feedbackDto, [FromServices] FeedbackService feedbackService) =>
            {
                var feedback = new Feedback(feedbackDto.Id, feedbackDto.UserId, feedbackDto.Rating,
                    feedbackDto.ProductId, feedbackDto.Message, feedbackDto.FeedbackDate);
                await feedbackService.UpdateFeedbackAsync(feedback);
                return Results.NoContent();
            }).RequireAuthorization();

        _app.MapDelete("/feedback/{id}",
            async ([FromServices] FeedbackService feedbackService, int id) =>
            {
                await feedbackService.DeleteFeedbackAsync(id);
                return Results.NoContent();
            }).RequireAuthorization();
    }
}