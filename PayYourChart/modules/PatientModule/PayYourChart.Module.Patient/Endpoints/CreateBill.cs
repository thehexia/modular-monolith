using FastEndpoints;

namespace PayYourChart.Module.Patient;

internal record class CreateBillRequest(long PatientId, DateTime DueDate, string Provider);
internal record class CreateBillDto(long BillId);

internal class CreateBill : Endpoint<CreateBillRequest, CreateBillDto>
{
    public override void Configure()
    {
        Post($"{ApiPath.Base}/patient/bill");
        AllowAnonymous();
    }

    public override Task HandleAsync(CreateBillRequest req, CancellationToken ct)
    {
        return base.HandleAsync(req, ct);
    }
}
