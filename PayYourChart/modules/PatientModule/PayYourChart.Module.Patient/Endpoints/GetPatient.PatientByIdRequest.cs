using FastEndpoints;
using FluentValidation;

namespace PayYourChart.Module.Patient;

internal record class PatientByIdRequest(long Id);

// I like to place validators in the same file as the class.
internal class PatientByIdValidator : Validator<PatientByIdRequest> 
{
    public PatientByIdValidator() 
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");
    }
}