using AYHF_Software_Architecture_And_Design.Application.Dtos;
using AYHF_Software_Architecture_And_Design.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MyProject.Domain.Models;

public class ProductRoutes
{
    private readonly WebApplication _app;

    public ProductRoutes(WebApplication app)
    {
        _app = app;
    }

    public void Configure()
    {
        _app.MapGet("/products", async ([FromServices] ProductService productService) => 
        {
            return await productService.GetAllProductsAsync();
        });

        _app.MapGet("/products/{id}", async (int id, [FromServices] ProductService productService) => 
        {
            var product = await productService.GetProductByIdAsync(id);
            return product == null ? Results.NotFound() : Results.Ok(product);
        });

        _app.MapPost("/products", async ([FromBody] ProductDto productDto, [FromServices] ProductService productService) => 
        {
            Product product = new Product(productDto.Id, productDto.Name, productDto.Description, productDto.LongDescription,
                                          productDto.Ingredients, productDto.Image, productDto.Rating, productDto.NumRatings,
                                          productDto.Price);
            await productService.AddProductAsync(product);
            return Results.Created($"/products/{product.Id}", product);
        });

        _app.MapPut("/products/{id}", async (int id, [FromBody] ProductDto productDto, [FromServices] ProductService productService) => 
        {
            Product product = new Product(productDto.Id, productDto.Name, productDto.Description, productDto.LongDescription,
                                          productDto.Ingredients, productDto.Image, productDto.Rating, productDto.NumRatings,
                                          productDto.Price);
            await productService.UpdateProductAsync(product);
            return Results.NoContent();
        });

        _app.MapDelete("/products/{id}", async (int id, [FromServices] ProductService productService) => 
        {
            await productService.DeleteProductAsync(id);
            return Results.NoContent();
        });
    }
}