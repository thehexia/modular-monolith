using FluentResults;
using MediatR;
using PayYourChart.Module.Item.Contracts;

namespace PayYourChart.Module.Patient;

internal record class AddItemToBillCommand(long PatientId, DateTime DateOfService) : IRequest;

internal class AddItemToBillHandler(IMediator mediator) : IRequestHandler<AddItemToBillCommand>
{
    readonly IMediator _mediator = mediator;

    public async Task Handle(AddItemToBillCommand request, CancellationToken ct)
    {
        Result<GetItemResponse> item = await _mediator.Send(new GetItemQuery(0), ct);
    }
}
