using AutoMapper;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PayYourChart.Module.Patient;

// If your request object isn't going to be used by multiple 
// endpoints, its ok to just declare it in the same file as the endpoint.
internal record class CreatePatientRequest(string FirstName, string LastName, string SSN, DateTime? DateOfBirth);

[HttpPost(ApiPath.Base)]
[AllowAnonymous]
[PostProcessor<CreatePatientExceptionProcessor>]
internal class CreatePatient(IPatientService service, IPatientDtoMapper mapper) : Endpoint<CreatePatientRequest, PatientDto>
{
    readonly IPatientService _service = service;
    readonly IPatientDtoMapper _mapper = mapper;

    public override async Task HandleAsync(CreatePatientRequest req, CancellationToken ct)
    {
        Patient patient = await _service.AddPatientAsync(req.FirstName, req.LastName, req.SSN, req.DateOfBirth);
        await SendAsync(_mapper.Get().Map<PatientDto>(patient));
    }
}


// Validators handle the input
internal class CreatePatientValidator : Validator<CreatePatientRequest> 
{
    public CreatePatientValidator() 
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.");

        RuleFor(x => x.SSN)
            .NotEmpty()
            .WithMessage("SSN is required.");
    }
}


// Here we use what's called a post-processor to handle errors in a special way unique to our request
internal class CreatePatientExceptionProcessor : IPostProcessor<CreatePatientRequest, PatientDto>
{
    public async Task PostProcessAsync(IPostProcessorContext<CreatePatientRequest, PatientDto> ctx, CancellationToken ct = default)
    {
        if (!ctx.HasExceptionOccurred)
            return;

        if (ctx.ExceptionDispatchInfo.SourceException.GetType() == typeof(DbUpdateException))
        {
            // Unique key violation
            if (ctx.ExceptionDispatchInfo.SourceException.InnerException?.Message.Contains("UNIQUE", StringComparison.OrdinalIgnoreCase) ?? false) 
            {
                ctx.MarkExceptionAsHandled(); //only if handling the exception here.    
                await ctx.HttpContext.Response.SendAsync(
                    new InternalErrorResponse
                    {
                        Status = "Conflict",
                        Code = 409,
                        Reason = "Could not create patient. A patient with the SSN and DOB already exists.",
                        Note = "A patient with the SSN and DOB already exists."
                    }, 409);      
                return;
            }
        }

        ctx.ExceptionDispatchInfo.Throw();
    }
}

