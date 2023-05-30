namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

/// <summary>
/// Represents a product entity in the system.
/// </summary>
public class Product
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the product.</param>
    /// <param name="name">The name of the product.</param>
    /// <param name="description">A short description of the product.</param>
    /// <param name="longDescription">A longer description of the product.</param>
    /// <param name="ingredients">The ingredients of the product.</param>
    /// <param name="image">The URL of the product's image.</param>
    /// <param name="rating">The rating of the product.</param>
    /// <param name="numRatings">The number of ratings the product has received.</param>
    /// <param name="price">The price of the product.</param>
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

    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    /// <value>The unique identifier.</value>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a short description of the product.
    /// </summary>
    /// <value>A short description of the product.</value>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets a longer description of the product.
    /// </summary>
    /// <value>A longer description of the product.</value>
    public string LongDescription { get; set; }

    /// <summary>
    /// Gets or sets the ingredients of the product.
    /// </summary>
    /// <value>The ingredients of the product.</value>
    public string Ingredients { get; set; }

    /// <summary>
    /// Gets or sets the URL of the product's image.
    /// </summary>
    /// <value>The URL of the product's image.</value>
    public string Image { get; set; }

    /// <summary>
    /// Gets or sets the rating of the product.
    /// </summary>
    /// <value>The rating of the product.</value>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the number of ratings the product has received.
    /// </summary>
    /// <value>The number of ratings the product has received.</value>
    public int NumRatings { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    /// <value>The price of the product.</value>
    public decimal Price { get; set; }
}
