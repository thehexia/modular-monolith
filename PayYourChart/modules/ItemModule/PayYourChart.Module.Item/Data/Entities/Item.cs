namespace PayYourChart.Module.Item;

public class Item
{
    public long Id { get; init; }
    public required string ItemCode { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
}
