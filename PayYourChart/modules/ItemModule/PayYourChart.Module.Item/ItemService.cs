namespace PayYourChart.Module.Item;

internal record class SpecialApprovalRequired(bool Required, string? Reason);

internal interface IItemService
{
    Task<SpecialApprovalRequired> DoesItemRequireSpecialApprovalAsync(string itemCode);
    SpecialApprovalRequired DoesItemRequireSpecialApproval(Item item);
}


internal class ItemService(IItemRepository item) : IItemService
{
    readonly IItemRepository _item = item;

    public async Task<SpecialApprovalRequired> DoesItemRequireSpecialApprovalAsync(string itemCode)
    {
        Item? item = await _item.GetItemAsync(itemCode);
        if (item != null)
        {
            return DoesItemRequireSpecialApproval(item);
        }
        throw new ItemNotFoundException(itemCode);
    }

    public SpecialApprovalRequired DoesItemRequireSpecialApproval(Item item)
    {
        if (item.Price >= 10000)
        {
            return new(true, "Item costs over $10,000");
        }
        return new(false, null);
    }
}
