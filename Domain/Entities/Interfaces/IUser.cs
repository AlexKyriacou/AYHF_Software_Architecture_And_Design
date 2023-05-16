namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

public interface IUser
{
    int Id { get; set; }
    string Username { get; set; }
    string Password { get; set; }

    string Email { get; set; }
    // Add any additional properties common to all users

    void Save();
    void Update();

    void Delete();
    // Add any additional methods common to all users
}