using FastEndpoints;
using MediatR;

namespace PayYourChart.Module.Patient;

internal record class AddItemToBillRequest(long PatientId);
internal record class AddItemToBillDto(long BillId);

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
        await _mediator.Send(new AddItemToBillCommand(req.PatientId, _time.GetUtcNow().DateTime), ct);
    }
}

