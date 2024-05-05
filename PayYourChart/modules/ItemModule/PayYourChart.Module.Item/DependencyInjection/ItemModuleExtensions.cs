using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PayYourChart.Module.Item;

public static partial class ItemModuleExtensions
{
    public static void AddItemModule(this IServiceCollection services, IList<Assembly> mediatrAssemblies) 
    {
        mediatrAssemblies.Add(typeof(ItemModuleExtensions).Assembly);

        services.AddDbContext<EfItemContext>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddSingleton<IItemDtoMapper, ItemDtoMapper>();
    }
}
