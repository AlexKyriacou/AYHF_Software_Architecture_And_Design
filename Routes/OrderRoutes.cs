using AYHF_Software_Architecture_And_Design.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using MyProject.Application.Services;
using MyProject.Domain.Models;

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

        _app.MapGet("/orders", async ([FromServices] OrderService orderService) => 
        {
            return await orderService.GetAllOrdersAsync();
        });

        _app.MapPost("/orders", async ([FromBody] OrderDto orderDto, [FromServices] OrderService orderService) => 
        {
            Order order = new Order(customer: orderDto.Customer, products: orderDto.Products);
            await orderService.AddOrderAsync(order);
            return Results.Created($"/orders/{order.Id}", order);
        });

        _app.MapPut("/orders/{id}", async ([FromBody] OrderDto orderDto, [FromServices] OrderService orderService) => 
        {
            Order order = new Order(customer: orderDto.Customer, products: orderDto.Products);
            await orderService.UpdateOrderAsync(order);
            return Results.NoContent();
        });

        _app.MapDelete("/orders/{id}", async (int id, [FromServices] OrderService orderService) => 
        {
            await orderService.DeleteOrderAsync(id);
            return Results.NoContent();
        });
    }
}