using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

public class UserService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IOrderRepository orderRepository)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
    }

    public Task<List<IUser>> GetUsersAsync()
    {
        return _userRepository.GetUsersAsync();
    }

    public Task<IUser?> GetUserByIdAsync(int id)
    {
        return _userRepository.GetUserByIdAsync(id);
    }

    public async Task<Customer?> GetCustomerWithOrdersByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null || user.Role != UserRole.Customer) return null;

        var customer = user as Customer;
        customer.Orders = await _orderRepository.GetAllOrdersByUserIdAsync(customer.Id);

        return customer;
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
        if (existingUser != null) throw new ArgumentException("User already exists");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        return await _userRepository.AddUserAsync(user);
    }

    public async Task<object> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new ArgumentException("Invalid email or password");

        var token = GenerateJwtForUser(user);

        return new { user, token };
    }

    private string GenerateJwtForUser(IUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superduperextraultramegaSecretKey@345"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim("role", ((int)user.Role).ToString())
        };

        var token = new JwtSecurityToken(
            "AYHF",
            "customers",
            claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}