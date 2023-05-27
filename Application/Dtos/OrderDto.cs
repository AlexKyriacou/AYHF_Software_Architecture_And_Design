using MyProject.Domain.Models;

namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public List<Product> Products { get; set; }
}