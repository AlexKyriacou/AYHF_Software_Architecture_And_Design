namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;

public interface IUser
{

    public string Role { get; set; }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }

    public void Save();


    public void Update();


    public void Delete();

}