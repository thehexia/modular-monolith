using FluentResults;
using MediatR;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Patient;

internal record class CreateBillCommand(long PatientId, DateTime DateOfService) : IRequest;

internal class CreateBillHandler(IMediator mediator) : IRequestHandler<CreateBillCommand>
{
    readonly IMediator _mediator = mediator;

    public async Task Handle(CreateBillCommand request, CancellationToken ct)
    {
        Result<GetItemResponse> item = await _mediator.Send(new GetItemQuery(0), ct);
    }
}
