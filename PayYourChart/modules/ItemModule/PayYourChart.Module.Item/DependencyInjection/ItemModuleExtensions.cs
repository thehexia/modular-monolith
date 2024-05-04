using Microsoft.Extensions.DependencyInjection;

namespace PayYourChart.Module.Item;

public static partial class ItemModuleExtensions
{
    public static void AddItemModule(this IServiceCollection services) 
    {
        services.AddDbContext<EfItemContext>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemDtoMapperFactory, ItemDtoMapperFactory>();
    }
}
