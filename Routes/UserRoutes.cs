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
        /// <summary>
        /// Handles the GET request to retrieve all users.
        /// </summary>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        _app.MapGet("/users",
            async ([FromServices] UserService userService) => await userService.GetUsersAsync());

        /// <summary>
        /// Handles the GET request to retrieve a specific user by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        _app.MapGet("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            var user = await userService.GetUserByIdAsync(id);
            return Results.Ok(user);
        });

        /// <summary>
        /// Handles the GET request to retrieve a customer and their orders by ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        _app.MapGet("/users/{id}/orders", async (int id, [FromServices] UserService userService) =>
        {
            var user = await userService.GetCustomerWithOrdersByIdAsync(id);
            return Results.Ok(user);
        });

        /// <summary>
        /// Handles the POST request to add a new user.
        /// </summary>
        /// <param name="userDto">The <see cref="UserDto"/> object representing the user to add.</param>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        /// <returns>A <see cref="IResult"/> object representing the result of the operation.</returns>
        _app.MapPost("/users", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            var user = CreateUser(userDto);
            if (user == null) return Results.BadRequest("Invalid Role");
            user.Id = await userService.AddUserAsync(user);
            return Results.Created($"/users/{user.Id}", user);
        });

        /// <summary>
        /// Handles the PUT request to update an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="userDto">The <see cref="UserDto"/> object representing the updated user.</param>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        /// <returns>A <see cref="IResult"/> object representing the result of the operation.</returns>
        _app.MapPut("/users/{id}", async (int id, [FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            if (id != userDto.Id) return Results.BadRequest();
            var user = CreateUser(userDto);
            if (user == null) return Results.BadRequest("Invalid Role");
            await userService.UpdateUserAsync(user);
            return Results.NoContent();
        }).RequireAuthorization();

        /// <summary>
        /// Handles the DELETE request to delete an existing user.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        /// <returns>A <see cref="IResult"/> object representing the result of the operation.</returns>
        _app.MapDelete("/users/{id}", async (int id, [FromServices] UserService userService) =>
        {
            await userService.DeleteUserAsync(id);
            return Results.NoContent();
        }).RequireAuthorization();

        /// <summary>
        /// Handles the POST request to register a new user.
        /// </summary>
        /// <param name="userDto">The <see cref="UserDto"/> object representing the user to register.</param>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        /// <returns>A <see cref="IResult"/> object representing the result of the operation.</returns>
        _app.MapPost("/users/register", async ([FromBody] UserDto userDto, [FromServices] UserService userService) =>
        {
            var user = CreateUser(userDto);
            if (user == null) return Results.BadRequest();
            user.Id = await userService.RegisterUserAsync(user);
            return Results.Created($"/users/{user.Id}", user);
        });

        /// <summary>
        /// Handles the POST request to log in a user.
        /// </summary>
        /// <param name="loginDto">The <see cref="LoginDto"/> object representing the user to log in.</param>
        /// <param name="userService">The <see cref="UserService"/> object.</param>
        /// <returns>A <see cref="IResult"/> object representing the result of the operation.</returns>
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

    /// <summary>
    /// Creates a new instance of a user based on the provided UserDto object.
    /// </summary>
    /// <param name="userDto">The UserDto object representing the user to create.</param>
    /// <returns>A new instance of a user.</returns>
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
