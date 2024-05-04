using FastEndpoints;

namespace PayYourChart.Module.Item;

internal record class GetRequiresSpecialApprovalRequest(string ItemCode);

/// <summary>
/// Returns true if the item code requires some type of special approval because
/// a certain criteria was met.
/// </summary>
internal class GetRequiresSpecialApproval : Endpoint<GetRequiresSpecialApproval, bool>
{

}
