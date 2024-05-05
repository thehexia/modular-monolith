using FluentResults;
using MediatR;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Item;

internal class GetItemByIdHandler(IItemService itemService, IItemRepository itemRepo, IItemDtoMapper mapper) : IRequestHandler<GetItemQuery, Result<GetItemResponse>>
{
    readonly IItemService _itemService = itemService;
    readonly IItemRepository _itemRepo = itemRepo;
    readonly IItemDtoMapper _mapper = mapper;

    public async Task<Result<GetItemResponse>> Handle(GetItemQuery request, CancellationToken cancellationToken)
    {
        Item? item = await _itemRepo.GetItemAsync(request.Id);
        if (item == null)
        {
            return Result.Fail($"Item with id {request.Id} not found.");
        }

        SpecialApprovalRequired approvalRequired = _itemService.DoesItemRequireSpecialApproval(item);

        var rsp = _mapper.Get().Map<GetItemResponse>(item);
        rsp.SpecialApprovalRequired = approvalRequired.Required;
        rsp.SpecialApprovalReason = approvalRequired.Reason;
        return Result.Ok(rsp);
    }
}
