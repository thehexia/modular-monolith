using FastEndpoints;
using FluentValidation;
using MediatR;

namespace PayYourChart.Module.Patient;

internal record class UpdateLineItemRequest(long LineItemId, DateTime DateOfService, string Provider, short Quantity);

internal class UpdateLineItemOnBill(IMediator mediator) : Endpoint<UpdateLineItemRequest>
{
    readonly IMediator _mediator = mediator;

    public override void Configure()
    {
        Put($"{ApiPath.Base}/patient/bill/item");
        Policies(Common.Policies.AdminCertPolicy);
    }

    public override async Task HandleAsync(UpdateLineItemRequest req, CancellationToken ct)
    {
        await _mediator.Send(new UpdateLineItemCommand(req.LineItemId, req.DateOfService, req.Provider, req.Quantity), ct);
        await SendOkAsync();
    }
}


internal class UpdateLineItemValidator : Validator<UpdateLineItemRequest> 
{
    public UpdateLineItemValidator() 
    {
        RuleFor(x => x.Provider)
            .NotEmpty()
            .WithMessage("Provider is required.");
        
        RuleFor(x => (int)x.Quantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Quantity must not be negative.");
    }
}

