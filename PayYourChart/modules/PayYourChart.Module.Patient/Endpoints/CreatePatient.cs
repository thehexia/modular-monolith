using FastEndpoints;
using FluentValidation;

namespace PayYourChart.Module.Patient;

// If your request object isn't going to be used by multiple 
// endpoints, its ok to just declare it in the same file as the endpoint.
internal record class CreatePatientRequest(string FirstName, string LastName, DateTime? DateOfBirth);

internal class CreatePatientValidator : Validator<CreatePatientRequest> 
{
    public CreatePatientValidator() 
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name must be given.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("First name must be given.");
    }
}


internal class CreatePatient(IPatientService service) : Endpoint<CreatePatientRequest>
{
    readonly IPatientService _service = service;

    public override async Task HandleAsync(CreatePatientRequest req, CancellationToken ct)
    {
        await _service.AddPatient(req.FirstName, req.LastName, req.DateOfBirth);
    }
}
