using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Models;

public class UserRoutes
{
    private readonly WebApplication _app;

    public UserRoutes(WebApplication app)
    {
        _app = app;
    }

    public void Configure()
    {
        _app.MapGet("/users", async ([FromServices] UserService userService) =>
        {
            return await userService.GetUsersAsync();
        });

        _app.MapGet("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            var user = await userService.GetUserByIdAsync(id);
            return Results.Ok(user);
        });

        _app.MapPost("/users", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            IUser user = new User { Id = userDto.Id, Username = userDto.Username };
            await userService.AddUserAsync(user);
            return Results.Created($"/users/{user.Id}", user);
        });

        _app.MapPut("/users/{id}", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            IUser user = new User { Id = userDto.Id, Username = userDto.Username };
            await userService.UpdateUserAsync(user);
            return Results.NoContent();
        });

        _app.MapDelete("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            await userService.DeleteUserAsync(id);
            return Results.NoContent();
        });
    }
}