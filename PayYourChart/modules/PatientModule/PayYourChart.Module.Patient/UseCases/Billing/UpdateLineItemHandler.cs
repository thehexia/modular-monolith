using MediatR;

namespace PayYourChart.Module.Patient;

internal record class UpdateLineItemCommand(long LineItemId, DateTime DateOfService, string Provider, short Quantity) : IRequest;

internal class UpdateLineItemHandler(IBillRepository billRepo) : IRequestHandler<UpdateLineItemCommand>
{
    readonly IBillRepository _billRepo = billRepo;

    public async Task Handle(UpdateLineItemCommand request, CancellationToken cancellationToken)
    {
        if (request.Quantity >= 1)
        {
            await _billRepo.UpdateLineItemAsync(request.LineItemId, request.DateOfService, request.Provider, request.Quantity);
        }
        else if (request.Quantity == 0)
        {
            await _billRepo.DeleteLineItemAsync(request.LineItemId);
        }

        await _billRepo.SaveChangesAsync();
    }
}
