using AutoMapper;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Item;

internal interface IItemDtoMapper
{
    IMapper Get();
}


internal class ItemDtoMapper : IItemDtoMapper
{
    readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg => 
        {
            cfg.CreateMap<Item, ItemDto>();
            cfg.CreateMap<Item, GetItemResponse>();
        }));

        
    public IMapper Get() => _mapper;
}
