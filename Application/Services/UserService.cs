using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

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

    public Task AddUserAsync(IUser user)
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
    
    public async Task RegisterUserAsync(IUser user)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new ArgumentException("User already exists");
        }
        
        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
        await _userRepository.AddUserAsync(user);
    }
    
    public async Task<IUser> LoginUserAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
        {
            throw new ArgumentException("Invalid email or password");
        }
        
        return user;
    }
}