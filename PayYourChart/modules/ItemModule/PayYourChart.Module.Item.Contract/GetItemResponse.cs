namespace PayYourChart.Module.Item.Contracts;

public record class GetItemResponse(long Id, string ItemCode, decimal Price, string? Description);
