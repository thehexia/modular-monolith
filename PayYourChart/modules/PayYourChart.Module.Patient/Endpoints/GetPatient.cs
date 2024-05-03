using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace PayYourChart.Module.Patient;

[HttpGet($"{ApiPath.Base}/{{id}}")]
[AllowAnonymous] // Temporarily allow this for testing
internal class GetPatient : Endpoint<PatientByIdRequest, PatientDto>
{
    public override async Task HandleAsync(PatientByIdRequest req, CancellationToken ct)
    {
        // This is a mock for now
        await SendAsync(new()
        {
            Id = req.Id,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(2020, 1, 1)
        });
    }
}
