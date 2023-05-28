namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

public class ShoppingCart
{
    private readonly List<Product> _items;

    public ShoppingCart()
    {
        _items = new List<Product>();
    }

    public void AddItem(Product product)
    {
        _items.Add(product);
    }

    public void RemoveItem(Product product)
    {
        _items.Remove(product);
    }

    public List<Product> GetItems()
    {
        return _items;
    }

    public void Clear()
    {
        _items.Clear();
    }
}