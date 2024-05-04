using AutoMapper;
using FastEndpoints;
using Microsoft.AspNetCore.Authorization;

namespace PayYourChart.Module.Patient;

[HttpGet($"{ApiPath.Base}/{{id}}")]
[AllowAnonymous] // Temporarily allow this for testing
[PostProcessor<GetPatientExceptionProcessor>]
internal class GetPatient(IPatientRepository patient, IPatientDtoMapperFactory mapper) : Endpoint<GetPatientByIdRequest, PatientDto>
{
    // I personally think its ok to skip the service if there is no business logic.
    readonly IPatientRepository _patient = patient;
    readonly IPatientDtoMapperFactory _mapper = mapper;

    public override async Task HandleAsync(GetPatientByIdRequest req, CancellationToken ct)
    {
        await SendAsync(_mapper.Get().Map<PatientDto>(await _patient.GetAsync(req.Id)));
    }
}


internal class GetPatientExceptionProcessor : IPostProcessor<GetPatientByIdRequest, PatientDto>
{
    public async Task PostProcessAsync(IPostProcessorContext<GetPatientByIdRequest, PatientDto> ctx, CancellationToken ct = default)
    {
        if (ctx.Response == null) 
        {
            await ctx.HttpContext.Response.SendAsync(
                new InternalErrorResponse
                {
                    Status = "No content",
                    Code = 204,
                    Reason = "No patient with the matching Id was found."
                }, 204); 
        }

        if (!ctx.HasExceptionOccurred)
            return;

        ctx.ExceptionDispatchInfo.Throw();
    }
}
