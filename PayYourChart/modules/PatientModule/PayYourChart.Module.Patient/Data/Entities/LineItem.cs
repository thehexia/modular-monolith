using System.ComponentModel.DataAnnotations;

namespace PayYourChart.Module.Patient;

internal class LineItem
{
    public long Id { get; init; }

    public long BillId { get; set; }

    /// <summary>
    /// The Id of the item from the item module that implements a catalog.
    /// </summary>
    public long ItemCatalogId { get; set; }

    /// <summary>
    /// Price at the time the line item was made.
    /// </summary>
    public decimal Price { get; set; }

    public short Quantity { get; set; } = 1;

    public DateTime DateOfService { get; set; }

    [MaxLength(128)]
    public required string Provider { get; set; }

    /// <summary>
    /// Short description of the service
    /// </summary>
    [MaxLength(256)]
    public required string Description { get; set; }
}
