using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

/// <summary>
/// Defines the contract for an admin user entity.
/// </summary>
public class Admin : IUser
{
    /// <summary>
    /// Gets or sets the unique identifier for the admin user.
    /// </summary>
    /// <value>The unique identifier for the admin user.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the admin user.
    /// </summary>
    /// <value>The name of the admin user.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the username of the admin user.
    /// </summary>
    /// <value>The username of the admin user.</value>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the password of the admin user.
    /// </summary>
    /// <value>The password of the admin user.</value>
    public string Password { get; set; }

    /// <summary>
    /// Gets or sets the email of the admin user.
    /// </summary>
    /// <value>The email of the admin user.</value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the role of the admin user.
    /// </summary>
    /// <value>The role of the admin user.</value>
    public UserRole Role { get; set; }
}