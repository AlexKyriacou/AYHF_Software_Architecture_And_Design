using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<IUser>> GetUsersAsync()
    {
        return _userRepository.GetUsersAsync();
    }

    public Task<IUser?> GetUserByIdAsync(int id)
    {
        return _userRepository.GetUserByIdAsync(id);
    }

    public Task<int> AddUserAsync(IUser user)
    {
        return _userRepository.AddUserAsync(user);
    }

    public Task UpdateUserAsync(IUser user)
    {
        return _userRepository.UpdateUserAsync(user);
    }

    public Task DeleteUserAsync(int id)
    {
        return _userRepository.DeleteUserAsync(id);
    }
    
    public async Task<int> RegisterUserAsync(IUser user)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new ArgumentException("User already exists");
        }
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
        return await _userRepository.AddUserAsync(user);
    }
    
    public async Task<Object> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new ArgumentException("Invalid email or password");
        }

        var token = GenerateJwtForUser(user);

        return new {user, token};
    }
    
    private string GenerateJwtForUser(IUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superduperextraultramegaSecretKey@345"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: "AYHF",
            audience: "customers",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}