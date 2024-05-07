using FastEndpoints;
using FluentResults;
using FluentValidation;
using MediatR;


namespace PayYourChart.Module.Patient;

internal record class AddItemToBillRequest(long BillId, long ItemId, string Provider, short Quantity = 1);
internal record class AddItemToBillDto(long BillId, long LineItemId);

internal class AddItemToBill(IMediator mediator, TimeProvider time) : Endpoint<AddItemToBillRequest, AddItemToBillDto>
{
    readonly IMediator _mediator = mediator;
    readonly TimeProvider _time = time; // Preferred way over DateTime.UtcNow

    public override void Configure()
    {
        Post($"{ApiPath.Base}/bill/item");
        Policies(Common.Policies.AdminCertPolicy);
    }


    public override async Task HandleAsync(AddItemToBillRequest req, CancellationToken ct)
    {
        Result<LineItem> lineItem = await _mediator.Send(new AddItemToBillCommand(req.BillId, req.ItemId, req.Provider, _time.GetUtcNow().DateTime, req.Quantity), ct);
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


internal class AddItemToBillValidator : Validator<AddItemToBillRequest> 
{
    public AddItemToBillValidator() 
    {
        RuleFor(x => x.Provider)
            .NotEmpty()
            .WithMessage("Provider is required.");
        
        RuleFor(x => (int)x.Quantity)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Quantity must be at least 1.");
    }
}

