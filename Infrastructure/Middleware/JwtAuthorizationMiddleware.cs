using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Middleware;

/// <summary>
/// Middleware that adds JWT authentication to incoming requests.
/// </summary>
public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _secretKey;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtAuthenticationMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="jwtSettings">The JSON web token (JWT) settings.</param>
    public JwtAuthenticationMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
    {
        _next = next;
        _secretKey = jwtSettings.Value.SecretKey;
    }

    /// <summary>
    /// Invokes the middleware to add JWT authentication to incoming requests.
    /// </summary>
    /// <param name="context">The context containing the request and response objects.</param>
    /// <returns>A task that represents the completion of this middleware.</returns>
    public async Task Invoke(HttpContext context)
    {
        var endpoint = context.GetEndpoint();

        if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() != null)
        {
            await _next(context);
            return;
        }

        // If the endpoint has IAuthorizeData metadata, then it requires authorization
        if (endpoint?.Metadata.GetMetadata<IAuthorizeData>() != null)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token is null)
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var userRole = jwtToken.Claims.First(x => x.Type == "role").Value;

                context.Items["User"] = new { Id = userId, Role = userRole };
            }
            catch
            {
                context.Response.StatusCode = 401; // Unauthorized
                return;
            }
        }

        await _next(context);
    }
}
