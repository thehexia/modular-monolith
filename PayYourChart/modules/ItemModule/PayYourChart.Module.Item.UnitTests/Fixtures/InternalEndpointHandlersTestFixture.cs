using AutoFixture;
using NSubstitute;

namespace PayYourChart.Module.Item.UnitTests;

public class InternalEndpointHandlersTestFixture
{
    public readonly Fixture f = new Fixture();
    internal readonly IItemRepository ItemRepository;
    internal readonly IItemService ItemService;
    internal readonly IItemDtoMapper Mapper;

    public InternalEndpointHandlersTestFixture()
    {
        ItemRepository = Substitute.For<IItemRepository>();
        ItemService = new ItemService(ItemRepository);
        Mapper = new ItemDtoMapper();

        f.Register(() => ItemRepository);
        f.Register(() => ItemService);
        f.Register(() => Mapper);
    }
}
