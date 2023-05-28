using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;
using AYHF_Software_Architecture_And_Design.Routes;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }); });

// Register your services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Define routes for Users
var userRoutes = new UserRoutes(app);
userRoutes.Configure();

// Define routes for Orders
var orderRoutes = new OrderRoutes(app);
orderRoutes.Configure();

// Define routes for Products
var productRoutes = new ProductRoutes(app);
productRoutes.Configure();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.Run();