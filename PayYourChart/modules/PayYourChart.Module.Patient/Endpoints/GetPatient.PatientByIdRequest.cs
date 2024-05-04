using FastEndpoints;
using FluentValidation;

namespace PayYourChart.Module.Patient;

internal record class GetPatientByIdRequest(long Id);

// I like to place validators in the same file as the class.
internal class PatientByIdValidator : Validator<GetPatientByIdRequest> 
{
    public PatientByIdValidator() 
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");
    }
}