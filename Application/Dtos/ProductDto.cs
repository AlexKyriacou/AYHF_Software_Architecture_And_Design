namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string LongDescription { get; set; }
    public string Ingredients { get; set; }
    public int Rating { get; set; }
    public int NumRatings { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; } = null!;
}