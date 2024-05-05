using FluentResults;
using MediatR;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Patient;

internal record class AddItemToBillCommand(long BillId, long ItemId, string Provider, DateTime DateOfService, short Quantity) : IRequest<Result<LineItem>>;

internal class AddItemToBillHandler(IMediator mediator, IBillRepository bill, IPatientDtoMapper mapper) : IRequestHandler<AddItemToBillCommand, Result<LineItem>>
{
    readonly IMediator _mediator = mediator;
    readonly IBillRepository _bill = bill;
    readonly IPatientDtoMapper _mapper = mapper;

    public async Task<Result<LineItem>> Handle(AddItemToBillCommand request, CancellationToken ct)
    {
        // First I want to get the full item information from my Item module
        Result<GetItemResponse> itemRsp = await _mediator.Send(new GetItemQuery(request.ItemId), ct);
        if (itemRsp.IsSuccess) 
        {
            LineItem item = _mapper.Get().Map<LineItem>(itemRsp.Value);
            item.DateOfService = request.DateOfService;
            item.BillId = request.BillId;
            item.Provider = request.Provider;
            item.Quantity = request.Quantity;

            await _bill.AddLineItemAsync(item);

            return Result.Ok(item);
        }

        return Result.Fail("Could not get information about line item from catalog.");
    }
}
