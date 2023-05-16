using MyProject.Domain.Models;

namespace MyProject.Infrastructure.Repositories;

public class UserRepository
{
    private readonly List<User> _userList;

    public UserRepository()
    {
        _userList = new List<User>();
    }

    public void SaveUser(User user)
    {
        _userList.Add(user);
    }

    public User GetUserById(int userId)
    {
        return _userList.Find(u => u.Id == userId);
    }

    public void UpdateUser(User user)
    {
        // Update the user in the database
    }

    public void DeleteUser(User user)
    {
        _userList.Remove(user);
    }
}