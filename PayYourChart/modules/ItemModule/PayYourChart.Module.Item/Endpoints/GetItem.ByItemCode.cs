using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Http;


namespace PayYourChart.Module.Item;

internal record class GetItemByItemCodeRequest(string ItemCode);


internal class GetItemByItemCode(IItemRepository item, IItemDtoMapper mapper) : Endpoint<GetItemByItemCodeRequest, ItemDto>
{
    readonly IItemRepository _item = item;
    readonly IItemDtoMapper _mapper = mapper;

    // You have to use Configure() instead of attributes if you want to do special swagger documentation
    public override void Configure()
    {
        Get($"{ApiPath.Base}/item-code/{{itemCode}}");
        AllowAnonymous();
        Description(b => b.Produces(204));
    }
    

    public override async Task HandleAsync(GetItemByItemCodeRequest req, CancellationToken ct)
    {
        Item? item = await _item.GetItemAsync(req.ItemCode);
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


internal class GetItemByItemCodeValidator : Validator<GetItemByItemCodeRequest> 
{
    public GetItemByItemCodeValidator() 
    {
        RuleFor(x => x.ItemCode)
            .Length(5, 10)
            .WithMessage("Item code must be between 5 and 10 characters.");
    }
}
