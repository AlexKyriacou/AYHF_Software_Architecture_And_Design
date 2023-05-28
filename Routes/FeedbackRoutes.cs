using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AYHF_Software_Architecture_And_Design.Routes
{
    public class FeedbackRoutes
    {
        private readonly WebApplication _app;

        public FeedbackRoutes(WebApplication app)
        {
            _app = app;
        }

        public void Configure()
        {
            _app.MapGet("/Feedbacks/{id}", async (int id, [FromServices] FeedbackService FeedbackService) =>
            {
                var Feedback = await FeedbackService.GetFeedbackByIdAsync(id);
                return Feedback == null ? Results.NotFound() : Results.Ok(Feedback);
            });

            _app.MapGet("/Products/{id}/Feedbacks", async (int id, [FromServices] FeedbackService FeedbackService) =>
            {
                var Feedback = await FeedbackService.GetAllFeedbackForProductAsync(id);
                return Feedback == null ? Results.NotFound() : Results.Ok(Feedback);
            });

            _app.MapGet("/Feedbacks",
                async ([FromServices] FeedbackService FeedbackService) => { return await FeedbackService.GetAllFeedbacksAsync(); });

            _app.MapPost("/Feedbacks", async ([FromBody] FeedbackDto FeedbackDto, [FromServices] FeedbackService FeedbackService) =>
            {
                var userRepo = new UserRepository();
                IUser? user = await userRepo.GetUserByIdAsync(FeedbackDto.CustomerId); //Check if provided user is a valid user
                if (user == null)
                {
                    return Results.BadRequest();
                }
                var feedback = new Feedback(FeedbackDto.Id, FeedbackDto.CustomerId, FeedbackDto.Rating, FeedbackDto.ProductId, FeedbackDto.Message, FeedbackDto.FeedbackDate);
                feedback.Id = await FeedbackService.AddFeedbackAsync(feedback);

                return Results.Created($"/Feedbacks/{feedback.Id}", feedback);
            }).RequireAuthorization();

            _app.MapPut("/Feedbacks/{id}", async ([FromBody] FeedbackDto FeedbackDto, [FromServices] FeedbackService FeedbackService) =>
            {
                var feedback = new Feedback(FeedbackDto.Id,FeedbackDto.CustomerId,FeedbackDto.Rating,FeedbackDto.ProductId,FeedbackDto.Message,FeedbackDto.FeedbackDate);
                await FeedbackService.UpdateFeedbackAsync(feedback);
                return Results.NoContent();
            }).RequireAuthorization();

            _app.MapDelete("/Feedbacks/{id}", async ([FromBody] FeedbackDto FeedbackDto, [FromServices] FeedbackService FeedbackService) =>
            {
                var feedback = new Feedback(FeedbackDto.Id, FeedbackDto.CustomerId, FeedbackDto.Rating, FeedbackDto.ProductId, FeedbackDto.Message, FeedbackDto.FeedbackDate);
                await FeedbackService.DeleteFeedbackAsync(feedback);
                return Results.NoContent();
            });
        }
    }
}
