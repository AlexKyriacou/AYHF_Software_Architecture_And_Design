using AYHF_Software_Architecture_And_Design.Domain.Entities.Interfaces;
using MyProject.Infrastructure.Repositories;

namespace MyProject.Domain.Models
{
    public class Customer : IUser
    {
        // Additional properties specific to customers
        public List<Order> Orders { get; set; }
        public DeliveryAddress DeliveryAddress { get; set; }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }


        private readonly UserRepository _userRepository;

        public Customer()
        {
            _userRepository = UserRepository.Instance;
            Role = "Customer";
        }

        public void Save()
        {
            _userRepository.AddUser(this);
        }

        public void Update()
        {
            _userRepository.UpdateUser(this);
        }

        public void Delete()
        {
            _userRepository.DeleteUser(this);
        }

        // Additional methods specific to customers
    }
}
