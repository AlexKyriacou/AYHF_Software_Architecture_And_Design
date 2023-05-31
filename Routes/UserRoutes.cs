using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc;

namespace AYHF_Software_Architecture_And_Design.Routes;

/// <summary>
/// This class contains routes related to users.
/// </summary>
public class UserRoutes
{
    private readonly WebApplication _app;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRoutes"/> class.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> object.</param>
    public UserRoutes(WebApplication app)
    {
        _app = app;
    }

    /// <summary>
    /// Configures the user routes.
    /// </summary>
    public void Configure()
    {
        _app.MapGet("/users",
            async ([FromServices] UserService userService) => await userService.GetUsersAsync());

        _app.MapGet("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            var user = await userService.GetUserByIdAsync(id);
            return Results.Ok(user);
        });

        _app.MapGet("/users/{id}/orders", async (int id, [FromServices] UserService userService) =>
        {
            var user = await userService.GetCustomerWithOrdersByIdAsync(id);
            return Results.Ok(user);
        });

        _app.MapPost("/users", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            var user = CreateUser(userDto);
            if (user == null) return Results.BadRequest("Invalid Role");
            user.Id = await userService.AddUserAsync(user);
            return Results.Created($"/users/{user.Id}", user);
        });

        _app.MapPut("/users/{id}", async (int id, [FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            if (id != userDto.Id) return Results.BadRequest();
            var user = CreateUser(userDto);
            if (user == null) return Results.BadRequest("Invalid Role");
            await userService.UpdateUserAsync(user);
            return Results.NoContent();
        }).RequireAuthorization();

        _app.MapDelete("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            await userService.DeleteUserAsync(id);
            return Results.NoContent();
        }).RequireAuthorization();

        _app.MapPost("/users/register", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            var user = CreateUser(userDto);
            if (user == null) return Results.BadRequest();
            user.Id = await userService.RegisterUserAsync(user);
            return Results.Created($"/users/{user.Id}", user);
        });

        _app.MapPost("/users/login", async ([FromBody] LoginDto loginDto, [FromServices] UserService userService) =>
        {
            try
            {
                var user = await userService.LoginUserAsync(loginDto.Email, loginDto.Password);
                return Results.Ok(user);
            }
            catch (ArgumentException ex)
            {
                return Results.BadRequest(ex.Message);
            }
        });
    }

    private static IUser? CreateUser(UserDto userDto)
    {
        IUser user;
        switch ((UserRole)userDto.Role)
        {
            case UserRole.Customer:
                user = new Customer
                {
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    Role = (UserRole)userDto.Role
                };
                break;
            case UserRole.Admin:
                user = new Admin
                {
                    Name = userDto.Name,
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    Role = (UserRole)userDto.Role
                };
                break;
            default:
                return null;
        }

        return user;
    }
}