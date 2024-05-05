using FastEndpoints;
using FluentResults;
using MediatR;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Patient;

internal record class AddItemToBillRequest(long BillId, long ItemId, string Provider);
internal record class AddItemToBillDto(long BillId, long LineItemId);

internal class AddItemToBill(IMediator mediator, TimeProvider time) : Endpoint<AddItemToBillRequest, AddItemToBillDto>
{
    readonly IMediator _mediator = mediator;
    readonly TimeProvider _time = time; // Preferred way over DateTime.UtcNow

    public override void Configure()
    {
        Post($"{ApiPath.Base}/patient/bill/item");
        AllowAnonymous();
    }


    public override async Task HandleAsync(AddItemToBillRequest req, CancellationToken ct)
    {
        Result<LineItem> lineItem = await _mediator.Send(new AddItemToBillCommand(req.BillId, req.ItemId, req.Provider, _time.GetUtcNow().DateTime), ct);
        if (lineItem.IsSuccess)
        {
            await SendOkAsync(new AddItemToBillDto(lineItem.Value.BillId, lineItem.Value.Id));
        }
        else
        {
            await SendErrorsAsync();
        }
    }
}

