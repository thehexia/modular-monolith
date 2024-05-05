using FastEndpoints;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace PayYourChart.Module.Patient;

internal record class CreateBillRequest(long PatientId);
internal record class CreateBillDto(long BillId);

internal class CreateBill(IMediator mediator, TimeProvider time) : Endpoint<CreateBillRequest, CreateBillDto>
{
    readonly IMediator _mediator = mediator;
    readonly TimeProvider _time = time; // Preferred way over DateTime.UtcNow

    public override void Configure()
    {
        Get($"{ApiPath.Base}/patient/bill");
        AllowAnonymous();
    }


    public override async Task HandleAsync(CreateBillRequest req, CancellationToken ct)
    {
        await _mediator.Send(new CreateBillCommand(req.PatientId, _time.GetUtcNow().DateTime), ct);
    }
}

