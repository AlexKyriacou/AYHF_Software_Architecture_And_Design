using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.OpenApi.Models;
using MyProject.Domain.Models;
using MyProject.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<UserService>();

var app = builder.Build();

// Configure the endpoints
app.MapGet("/", () => "Hello World!");
app.MapGet("/users", async () =>
{
    var userService = app.Services.GetService(typeof(UserService)) as UserService;
    return await userService.GetUsers();
});

app.MapGet("/users/{id}", async (int id) =>
{
    var userService = app.Services.GetService(typeof(UserService)) as UserService;
    var user = await userService.GetUserById(id);
    return user == null ? Results.NotFound() : Results.Ok(user);
});

app.MapPost("/users", async (UserDto userDto) =>
{
    var userService = app.Services.GetService(typeof(UserService)) as UserService;
    // Conversion from DTO to domain object would happen here
    IUser user = new User { Id = userDto.Id, Username = userDto.Username };
    await userService.AddUser(user);
    return Results.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id}", async (int id, UserDto userDto) =>
{
    var userService = app.Services.GetService(typeof(UserService)) as UserService;
    // Conversion from DTO to domain object would happen here
    IUser user = new User { Id = userDto.Id, Username = userDto.Username };
    await userService.UpdateUser(user);
    return Results.NoContent();
});

app.MapDelete("/users/{id}", async (int id) =>
{
    var userService = app.Services.GetService(typeof(UserService)) as UserService;
    await userService.DeleteUser(id);
    return Results.NoContent();
});


// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.Run();