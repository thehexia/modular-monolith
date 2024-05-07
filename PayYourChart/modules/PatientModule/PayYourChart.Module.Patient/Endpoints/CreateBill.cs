using FastEndpoints;

namespace PayYourChart.Module.Patient;

internal record class CreateBillRequest(long PatientId, DateTime DueDate, string Provider);
internal record class CreateBillDto(long BillId);

internal class CreateBill(IBillRepository bill) : Endpoint<CreateBillRequest, CreateBillDto>
{
    readonly IBillRepository _bill = bill;

    public override void Configure()
    {
        Post($"{ApiPath.Base}/bill");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateBillRequest req, CancellationToken ct)
    {
        Bill bill = await _bill.AddAsync(req.PatientId, req.DueDate, req.Provider);
        await _bill.SaveChangesAsync();
        await SendAsync(new(bill.Id));
    }
}