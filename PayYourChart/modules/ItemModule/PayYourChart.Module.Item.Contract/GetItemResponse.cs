namespace PayYourChart.Module.Item.Contracts;

public class GetItemResponse
{
    public long Id { get; init; }
    public required string ItemCode { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
    public bool SpecialApprovalRequired { get; set; }
    public string? SpecialApprovalReason { get; set; }
}
