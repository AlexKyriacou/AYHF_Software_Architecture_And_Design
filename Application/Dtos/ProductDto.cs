namespace AYHF_Software_Architecture_And_Design.Application.Dtos;

/// <summary>
/// Represents a product data transfer object.
/// </summary>
public class ProductDto
{
    /// <summary>
    /// Gets or sets the identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Gets or sets the long description of the product.
    /// </summary>
    public string LongDescription { get; set; }

    /// <summary>
    /// Gets or sets the ingredients of the product.
    /// </summary>
    public string Ingredients { get; set; }

    /// <summary>
    /// Gets or sets the rating of the product.
    /// </summary>
    public int Rating { get; set; }

    /// <summary>
    /// Gets or sets the number of ratings of the product.
    /// </summary>
    public int NumRatings { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the image of the product.
    /// </summary>
    public string Image { get; set; } = null!;
}
