using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc;

namespace AYHF_Software_Architecture_And_Design.Routes;

public class OrderRoutes
{
    private readonly WebApplication _app;

    public OrderRoutes(WebApplication app)
    {
        _app = app;
    }

    public void Configure()
    {
        _app.MapGet("/orders/{id}", async (int id, [FromServices] OrderService orderService) =>
        {
            var order = await orderService.GetOrderByIdAsync(id);
            return order == null ? Results.NotFound() : Results.Ok(order);
        });

        _app.MapGet("/orders",
            async ([FromServices] OrderService orderService) => { return await orderService.GetAllOrdersAsync(); });

        _app.MapPost("/orders", async ([FromBody] OrderDto orderDto, [FromServices] OrderService orderService) =>
        {
            var order = new Order(orderDto.Customer, orderDto.Products);
            await orderService.AddOrderAsync(order);
            return Results.Created($"/orders/{order.Id}", order);
        }).RequireAuthorization();

        _app.MapPut("/orders/{id}", async ([FromBody] OrderDto orderDto, [FromServices] OrderService orderService) =>
        {
            var order = new Order(orderDto.Customer, orderDto.Products);
            await orderService.UpdateOrderAsync(order);
            return Results.NoContent();
        }).RequireAuthorization();

        _app.MapDelete("/orders/{id}", async (int id, [FromServices] OrderService orderService) =>
        {
            await orderService.DeleteOrderAsync(id);
            return Results.NoContent();
        }).RequireAuthorization();
    }
}