using FastEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace PayYourChart.Module.Item;


internal record class GetItemByIdRequest(long Id);


[HttpGet($"{ApiPath.Base}/{{id}}")]
[AllowAnonymous]
internal class GetItemById(IItemRepository item) : Endpoint<GetItemByIdRequest, ItemDto>
{
    readonly IItemRepository _item = item;
    readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Item, ItemDto>()));

    public override async Task HandleAsync(GetItemByIdRequest req, CancellationToken ct)
    {
        
        
    }
}
