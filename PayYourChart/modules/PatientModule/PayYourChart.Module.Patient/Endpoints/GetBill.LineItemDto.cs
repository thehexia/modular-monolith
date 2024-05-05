namespace PayYourChart.Module.Patient;

public class LineItemDto
{
    /// <summary>
    /// Price at the time the line item was made.
    /// </summary>
    public decimal Price { get; init; }

    public short Quantity { get; init; }

    public DateTime DateOfService { get; init; }

    public required string Provider { get; init; }

    /// <summary>
    /// Short description of the service
    /// </summary>
    public required string Description { get; init; }
}
