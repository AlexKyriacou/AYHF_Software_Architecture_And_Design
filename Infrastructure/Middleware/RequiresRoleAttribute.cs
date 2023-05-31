using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Middleware;

/// <summary>
/// Represents an attribute for authentication of roles.
/// </summary>
public class RequiresRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _role;

    /// <summary>
    /// Initializes a new instance of the <see cref="RequiresRoleAttribute"/> class.
    /// </summary>
    /// <param name="role">The access-level role required to run the method/ class.</param>
    public RequiresRoleAttribute(string role)
    {
        _role = role;
    }

    /// <summary>
    /// Checks if the user in the context has the required role and adds a forbidden response if not.
    /// </summary>
    /// <param name="context">The context containing the authorization information.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = (dynamic)context.HttpContext.Items["User"];
        if (user is null || user.Role != _role) context.Result = new StatusCodeResult(403); // Forbidden
    }
}