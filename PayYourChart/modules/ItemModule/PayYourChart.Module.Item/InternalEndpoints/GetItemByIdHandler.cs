using FluentResults;
using MediatR;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Item;

internal class GetItemByIdHandler(IItemRepository item, IItemDtoMapper mapper) : IRequestHandler<GetItemQuery, Result<GetItemResponse>>
{
    readonly IItemRepository _item = item;
    readonly IItemDtoMapper _mapper = mapper;

    public async Task<Result<GetItemResponse>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        Item? item = await _item.GetItemAsync(request.Id);
        return Result.Ok(_mapper.Get().Map<GetItemResponse>(item));
    }
}
