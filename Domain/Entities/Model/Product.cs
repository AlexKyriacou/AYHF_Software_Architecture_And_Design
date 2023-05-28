namespace MyProject.Domain.Models;

public class Product
{
    public Product(int id, string name, string description, string longDescription, string ingredients, string image, int rating, int numRatings, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        LongDescription = longDescription;
        Ingredients = ingredients;
        Image = image;
        Rating = rating;
        NumRatings = numRatings;
        Price = price;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string LongDescription { get; set; }
    public string Ingredients { get; set; }
    public string Image { get; set; }
    public int Rating { get; set; }
    public int NumRatings { get; set; }
    public decimal Price { get; set; }
}