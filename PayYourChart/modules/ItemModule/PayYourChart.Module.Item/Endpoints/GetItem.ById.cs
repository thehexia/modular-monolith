using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace PayYourChart.Module.Item;


internal record class GetItemByIdRequest(long Id);


internal class GetItemById(IItemRepository item, IItemDtoMapperFactory mapper) : Endpoint<GetItemByIdRequest, ItemDto>
{
    readonly IItemRepository _item = item;
    readonly IItemDtoMapperFactory _mapper = mapper;

    // You have to use Configure() instead of attributes if you want to do special swagger documentation
    public override void Configure()
    {
        Get($"{ApiPath.Base}/{{id}}");
        AllowAnonymous();
        Description(b => b.Produces(204));
    }


    public override async Task HandleAsync(GetItemByIdRequest req, CancellationToken ct)
    {
        Item? item = await _item.GetItemAsync(req.Id);
        if (item != null)
        {
            await SendAsync(_mapper.Get().Map<ItemDto>(item));
        }
        else
        {
            await SendNoContentAsync();
        }
    }
}
