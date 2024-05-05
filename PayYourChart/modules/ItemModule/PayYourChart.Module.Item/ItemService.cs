﻿namespace PayYourChart.Module.Item;

internal record class SpecialApprovalRequired(bool Required, string? Reason);

internal interface IItemService
{
    Task<IEnumerable<Item>> GetCardiacRehabItemsAsync();
    Task<SpecialApprovalRequired> DoesItemRequireSpecialApprovalAsync(string itemCode);
    SpecialApprovalRequired DoesItemRequireSpecialApproval(Item item);
}


internal class ItemService(IItemRepository item) : IItemService
{
    readonly IItemRepository _item = item;

    public async Task<IEnumerable<Item>> GetCardiacRehabItemsAsync()
    {
        IEnumerable<Item?> items = [ await _item.GetItemAsync("93797"), await _item.GetItemAsync("93798") ];
        return items.Where(i => i != null) as IEnumerable<Item>;
    }

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
