using System.ComponentModel.DataAnnotations;

namespace PayYourChart.Module.Item;

public class Item
{
    public long Id { get; init; }
    
    [StringLength(32)]
    public required string ItemCode { get; init; }
    
    public decimal Price { get; init; }

    [StringLength(256)]
    public string? Description { get; init; }
}
