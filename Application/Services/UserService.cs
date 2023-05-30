using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Enums;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace AYHF_Software_Architecture_And_Design.Application.Services;

/// <summary>
/// Represents a service for managing users.
/// </summary>
public class UserService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor for UserService class.
    /// </summary>
    /// <param name="userRepository">An interface for user repositories.</param>
    /// <param name="orderRepository">An interface for order repositories.</param>
    public UserService(IUserRepository userRepository, IOrderRepository orderRepository)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Gets a list of all user objects asynchronously.
    /// </summary>
    /// <returns>A Task wrapping a List of IUser objects.</returns>
    public Task<List<IUser>> GetUsersAsync()
    {
        return _userRepository.GetUsersAsync();
    }

    /// <summary>
    /// Gets an IUser object by its id asynchronously.
    /// </summary>
    /// <param name="id">An IUser's id.</param>
    /// <returns>A Task wrapping an IUser object.</returns>
    public Task<IUser?> GetUserByIdAsync(int id)
    {
        return _userRepository.GetUserByIdAsync(id);
    }

    /// <summary>
    /// Gets a Customer object with orders by its id asynchronously.
    /// </summary>
    /// <param name="id">A Customer's id.</param>
    /// <returns>A completed Task wrapping a Customer object with orders.</returns>
    public async Task<Customer?> GetCustomerWithOrdersByIdAsync(int id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);

        if (user == null || user.Role != UserRole.Customer) return null;

        var customer = user as Customer;
        customer.Orders = await _orderRepository.GetAllOrdersByUserIdAsync(customer.Id);

        return customer;
    }

    /// <summary>
    /// Adds an IUser object asynchronously.
    /// </summary>
    /// <param name="user">An IUser object.</param>
    /// <returns>A Task wrapping the id of the IUser object added.</returns>
    public Task<int> AddUserAsync(IUser user)
    {
        return _userRepository.AddUserAsync(user);
    }

    /// <summary>
    /// Updates an IUser object asynchronously.
    /// </summary>
    /// <param name="user">An IUser object.</param>
    /// <returns>A completed Task.</returns>
    public Task UpdateUserAsync(IUser user)
    {
        return _userRepository.UpdateUserAsync(user);
    }

    /// <summary>
    /// Deletes an IUser object by its id asynchronously.
    /// </summary>
    /// <param name="id">An IUser's id.</param>
    /// <returns>A completed Task.</returns>
    public Task DeleteUserAsync(int id)
    {
        return _userRepository.DeleteUserAsync(id);
    }

    /// <summary>
    /// Registers a user asynchronously.
    /// </summary>
    /// <param name="user">An IUser object representing a user.</param>
    /// <returns>A Task wrapping the id of the IUser object added.</returns>
    /// <exception cref="ArgumentException">The user already exists.</exception>
    public async Task<int> RegisterUserAsync(IUser user)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null) throw new ArgumentException("User already exists");

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        return await _userRepository.AddUserAsync(user);
    }

    /// <summary>
    /// Logs in a user asynchronously.
    /// </summary>
    /// <param name="email">The user's email.</param>
    /// <param name="password">The user's password.</param>
    /// <returns>A Task wrapping an object with the user and token.</returns>
    /// <exception cref="ArgumentException">Either the email or password is invalid.</exception>
    public async Task<object> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            throw new ArgumentException("Invalid email or password");

        var token = GenerateJwtForUser(user);

        return new { user, token };
    }

    /// <summary>
    /// Generates a JSON web token for a user.
    /// </summary>
    /// <param name="user">An IUser object representing a user.</param>
    /// <returns>A JSON web token.</returns>
    private string GenerateJwtForUser(IUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superduperextraultramegaSecretKey@345"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
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
