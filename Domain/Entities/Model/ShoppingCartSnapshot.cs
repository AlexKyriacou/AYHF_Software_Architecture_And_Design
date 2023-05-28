namespace AYHF_Software_Architecture_And_Design.Domain.Entities.Model;

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