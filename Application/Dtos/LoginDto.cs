namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

/// <summary> 
/// Login Data transfer object containing Email and Password properties
/// </summary>
public class LoginDto
{
    /// <summary>
    /// Email property representing email information of user.
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// Password property representing password of user..
    /// </summary>
    public string Password { get; set; }
}