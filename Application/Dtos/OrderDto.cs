using AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public List<Product> Products { get; set; }
}