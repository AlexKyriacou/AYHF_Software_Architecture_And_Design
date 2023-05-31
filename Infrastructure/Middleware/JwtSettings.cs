namespace AYHF_Software_Architecture_And_Design.Infrastructure.Middleware;

/// <summary>
/// Represents a settings object for JSON web token authentication middleware.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the secret key used to encrypt and decrypt JWT tokens.
    /// </summary>
    public string SecretKey { get; set; }
}