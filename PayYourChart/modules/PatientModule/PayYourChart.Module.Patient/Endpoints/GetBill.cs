using FastEndpoints;
using FluentResults;
using MediatR;

namespace PayYourChart.Module.Patient;

internal record class GetBillRequest(long BillId);


internal class GetBill(IMediator mediator, IPatientDtoMapper mapper) : Endpoint<GetBillRequest, BillDto>
{
    readonly IMediator _mediator = mediator;
    readonly IPatientDtoMapper _mapper = mapper;

    public override void Configure()
    {
        Get($"{ApiPath.Base}/patient/bill/{{billId}}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetBillRequest req, CancellationToken ct)
    {
        Result<CollateBillResponse> collate = await _mediator.Send(new CollateBillQuery(req.BillId));
        if (collate.IsSuccess)
        {
            await SendAsync(_mapper.Get().Map<BillDto>(collate.Value));
        }
        else
        {
            throw new InvalidOperationException("Could not collate bill.");
        }
    }
}
