using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;
using MyProject.Domain.Models;
using MyProject.Infrastructure.Repositories;

namespace AYHF_Software_Architecture_And_Design.Application.Services
{
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

        public Task<IUser> GetUserByIdAsync(int id)
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
    }
}