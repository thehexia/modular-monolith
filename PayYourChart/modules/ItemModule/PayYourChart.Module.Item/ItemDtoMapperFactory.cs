using AutoMapper;

namespace PayYourChart.Module.Item;

internal interface IItemDtoMapperFactory
{
    IMapper Get();
}


internal class ItemDtoMapperFactory : IItemDtoMapperFactory
{
    readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Item, ItemDto>()));
    public IMapper Get() => _mapper;
}
