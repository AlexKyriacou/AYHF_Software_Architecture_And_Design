namespace MyProject.Domain.Models;

public class ShoppingCartSnapshot
{
    public ShoppingCartSnapshot(DateTime snapshotDate, List<Product> items)
    {
        SnapshotDate = snapshotDate;
        Items = items;
    }

    public int Id { get; set; }
    public DateTime SnapshotDate { get; set; }
    public List<Product> Items { get; set; }
}