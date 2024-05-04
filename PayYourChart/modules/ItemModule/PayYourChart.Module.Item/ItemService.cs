namespace PayYourChart.Module.Item;

internal interface IItemService
{
    Task<IEnumerable<Item>> GetCardiacRehabItemsAsync();
    Task<bool> RequiresSpecialApproval(string ItemCode);
}


internal class ItemService(IItemRepository item) : IItemService
{
    readonly IItemRepository _item = item;

    public async Task<IEnumerable<Item>> GetCardiacRehabItemsAsync()
    {
        IEnumerable<Item?> items = [ await _item.GetItemAsync("93797"), await _item.GetItemAsync("93798") ];
        return items.Where(i => i != null) as IEnumerable<Item>;
    }

    public Task<bool> RequiresSpecialApproval(string ItemCode)
    {
        throw new NotImplementedException();
    }
}
