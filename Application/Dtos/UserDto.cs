namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

/// <summary>
/// Represents a user data transfer object.
/// </summary>
public class UserDto
{
    /// <summary>
    /// Gets or sets the identifier of the user.
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Gets or sets the name of the user.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Gets or sets the username of the user.
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }
    /// <summary>
    /// Gets or sets the role of the user.
    /// </summary>
    public int Role { get; set; }
}