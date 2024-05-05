namespace PayYourChart.Module.Item.Contracts;

public record class DoesItemRequireSpecialApprovalResponse(bool Required, string? Reason);
