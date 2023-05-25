using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

namespace AYHF_Software_Architecture_And_Design.Infrastructure.Interfaces;

public interface IUserRepository
{
    List<IUser> GetUsers();
    IUser GetUserById(int id);
    void AddUser(IUser user);
    void UpdateUser(IUser user);
    void DeleteUser(IUser user);
}
