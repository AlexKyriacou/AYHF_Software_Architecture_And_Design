using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
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

            _app.MapGet("/Feedbacks",
                async ([FromServices] FeedbackService FeedbackService) => { return await FeedbackService.GetAllFeedbacksAsync(); });

            _app.MapPost("/Feedbacks", async ([FromBody] FeedbackDto FeedbackDto, [FromServices] FeedbackService FeedbackService) =>
            {
                Customer user = new Customer
                {
                    Id = FeedbackDto.Customer.Id,
                    Name = FeedbackDto.Customer.Name,
                    Username = FeedbackDto.Customer.Username,
                    Email = FeedbackDto.Customer.Email,
                    Password = FeedbackDto.Customer.Password,
                    Role = FeedbackDto.Customer.Role
                };
                var Feedback = new Feedback(user, FeedbackDto.Message);
                await FeedbackService.AddFeedbackAsync(Feedback);
                return Results.Created($"/Feedbacks/{Feedback.Id}", Feedback);
            });

            _app.MapPut("/Feedbacks/{id}", async ([FromBody] FeedbackDto FeedbackDto, [FromServices] FeedbackService FeedbackService) =>
            {
                Customer user = new Customer
                {
                    Id = FeedbackDto.Customer.Id,
                    Name = FeedbackDto.Customer.Name,
                    Username = FeedbackDto.Customer.Username,
                    Email = FeedbackDto.Customer.Email,
                    Password = FeedbackDto.Customer.Password,
                    Role = FeedbackDto.Customer.Role
                };
                var Feedback = new Feedback(user, FeedbackDto.Message);
                await FeedbackService.UpdateFeedbackAsync(Feedback);
                return Results.NoContent();
            });

            _app.MapDelete("/Feedbacks/{id}", async (Feedback Feedback, [FromServices] FeedbackService FeedbackService) =>
            {
                await FeedbackService.DeleteFeedbackAsync(Feedback);
                return Results.NoContent();
            });
        }
    }
}
