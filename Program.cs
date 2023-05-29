using System.Text;
using AYHF_Software_Architecture_And_Design.Application.Services;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Scraper;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using AYHF_Software_Architecture_And_Design.Infrastructure.Middleware;
using AYHF_Software_Architecture_And_Design.Infrastructure.Repositories;
using AYHF_Software_Architecture_And_Design.Routes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("OpenCorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

// Configure services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

//JWT Authentication
var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
builder.Services.Configure<JwtSettings>(jwtSettingsSection);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "AYHF",
            ValidAudience = "customers",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });
builder.Services.AddControllers();

// Register your services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IFeedbackRepository, FeedbackRepository>();
builder.Services.AddScoped<IScraper, ColesScraper>();
builder.Services.AddScoped<FeedbackService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

// Use CORS policy in your application
app.UseCors("OpenCorsPolicy");

// Use authentication and the JwtAuthenticationMiddleware
app.UseAuthentication();
app.UseMiddleware<JwtAuthenticationMiddleware>();

// Define routes for Users
var userRoutes = new UserRoutes(app);
userRoutes.Configure();

// Define routes for Orders
var orderRoutes = new OrderRoutes(app);
orderRoutes.Configure();

// Define routes for Products
var productRoutes = new ProductRoutes(app);
productRoutes.Configure();

//Define routes for Feedback
var feedbackRoutes = new FeedbackRoutes(app);
feedbackRoutes.Configure();

// Configure Swagger
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

app.Run();