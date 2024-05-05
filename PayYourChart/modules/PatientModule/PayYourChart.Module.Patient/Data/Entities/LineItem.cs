namespace PayYourChart.Module.Patient;

internal class LineItem
{
    public long Id { get; init; }

    public long BillId { get; init; }

    /// <summary>
    /// The Id of the item from the item module that implements a catalog.
    /// </summary>
    public long ItemCatalogId { get; init; }

    /// <summary>
    /// Price at the time the line item was made.
    /// </summary>
    public decimal Price { get; init; }

    public DateTime DateOfService { get; init; }

    public required string Provider { get; init; }

    /// <summary>
    /// Short description of the service
    /// </summary>
    public required string Description { get; init; }
}
