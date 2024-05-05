using FluentResults;
using MediatR;

namespace PayYourChart.Module.Patient;

internal record class CollateBillQuery(long BillId) : IRequest<Result<CollateBillResponse>>;
internal record class CollateBillResponse(long Id, long PatientId, DateTime DueDate, string Provider, IEnumerable<LineItem> LineItems, decimal GrossTotal);

internal class CollateBillHandler(IBillRepository bill) : IRequestHandler<CollateBillQuery, Result<CollateBillResponse>>
{
    readonly IBillRepository _bill = bill;

    public async Task<Result<CollateBillResponse>> Handle(CollateBillQuery request, CancellationToken cancellationToken)
    {
        Bill? bill = await _bill.GetAsync(request.BillId);

        if (bill != null)
        {
            CollateBillResponse rsp = new CollateBillResponse(request.BillId, 
                bill.PatientId, 
                bill.DueDate,
                bill.Provider, 
                bill.LineItems, 
                bill.LineItems.Sum(l => l.Price * l.Quantity));
            
            return Result.Ok(rsp);
        }
        else
        {
            return Result.Fail($"No bill with id {request.BillId} found.");
        }
    }
}
