namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class Product
{
    public Product(int id, string name, decimal price, string description, string[] images)
    {
        Id = id;
        Name = name;
        Price = price;
        Description = description;
        Images = images;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string[] Images { get; set; }
}