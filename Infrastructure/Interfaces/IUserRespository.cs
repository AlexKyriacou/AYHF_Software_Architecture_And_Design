using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<IUser?> GetUserByIdAsync(int id);
    Task<int> AddUserAsync(IUser user);
    Task<IUser?> GetUserByEmailAsync(string email);
    Task<List<IUser>> GetUsersAsync();
    Task UpdateUserAsync(IUser user);
    Task DeleteUserAsync(int id);
}