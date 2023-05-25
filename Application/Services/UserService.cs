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

        public Task<List<IUser>> GetUsers()
        {
            return Task<List<IUser>>.FromResult(_userRepository.GetUsers());
        }

        public Task<IUser> GetUserById(int id)
        {
            IUser user = _userRepository.GetUserById(id);
            return Task.FromResult(user);
        }


        public Task AddUser(IUser user)
        {
            _userRepository.AddUser(user);
            return Task.CompletedTask;
        }

        public Task UpdateUser(IUser user)
        {
            _userRepository.UpdateUser(user);
            return Task.CompletedTask;
        }

        public async Task DeleteUser(int id)
        {
            IUser userToDelete = await GetUserById(id);
            if (userToDelete != null)
            {
                _userRepository.DeleteUser(userToDelete);
            }
        }
    }
}