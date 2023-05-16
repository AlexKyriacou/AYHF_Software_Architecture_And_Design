namespace DefaultNamespace;

public abstract class ShoppingPage : IPage
{
    public string PageTitle { get; set; }

    public abstract void Render();

    // Common functionalities and partial implementation for shopping pages
    protected ShoppingCart ShoppingCart { get; set; }
    protected void AddToCart(Product product)
    {
        // Logic to add a product to the shopping cart
    }
}
