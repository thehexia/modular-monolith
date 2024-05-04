using Microsoft.EntityFrameworkCore;
using PayYourChart.Module.Common;

namespace PayYourChart.Module.Item;


internal interface IItemRepository
{
    Task<Item?> GetItemAsync(long Id);
    Task<Item?> GetItemAsync(string itemCode);
}


internal class ItemRepository(EfItemContext context) : EfRepositoryBase<EfItemContext>(context), IItemRepository
{
    public async Task<Item?> GetItemAsync(long Id)
    {
        return await _context.Item.Where(i => i.Id == Id).SingleOrDefaultAsync();
    }

    public async Task<Item?> GetItemAsync(string itemCode)
    {
        return await _context.Item.Where(i => i.ItemCode == itemCode).SingleOrDefaultAsync();
    }
}
