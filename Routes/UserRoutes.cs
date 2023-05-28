using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc;

namespace AYHF_Software_Architecture_And_Design.Routes;

public class UserRoutes
{
    private readonly WebApplication _app;

    public UserRoutes(WebApplication app)
    {
        _app = app;
    }

    public void Configure()
    {
        _app.MapGet("/users",
            async ([FromServices] UserService userService) => { return await userService.GetUsersAsync(); });

        _app.MapGet("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            var user = await userService.GetUserByIdAsync(id);
            return Results.Ok(user);
        });

        _app.MapPost("/users", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            IUser user = new User
            {
                Id = userDto.Id, Name = userDto.Name, Username = userDto.Username, Email = userDto.Email,
                Password = userDto.Password, Role = userDto.Role
            };
            await userService.AddUserAsync(user);
            return Results.Created($"/users/{user.Id}", user);
        });

        _app.MapPut("/users/{id}", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            IUser user = new User
            {
                Id = userDto.Id, Name = userDto.Name, Username = userDto.Username, Email = userDto.Email,
                Password = userDto.Password, Role = userDto.Role
            };
            await userService.UpdateUserAsync(user);
            return Results.NoContent();
        });

        _app.MapDelete("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            await userService.DeleteUserAsync(id);
            return Results.NoContent();
        });
        
        _app.MapPost("/users/register", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            IUser user = new User
            {
                Id = userDto.Id, Name = userDto.Name, Username = userDto.Username, Email = userDto.Email,
                Password = userDto.Password, Role = userDto.Role
            };
            await userService.RegisterUserAsync(user);
            return Results.Created($"/users/{user.Id}", user);
        });
        
        _app.MapPost("/users/login", async ([FromBody] LoginDto loginDto, [FromServices] UserService userService) =>
        {
            await userService.LoginUserAsync(loginDto.Email, loginDto.Password);
            return Results.Ok();
        });
    }
}