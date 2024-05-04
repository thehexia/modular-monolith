using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace PayYourChart.Module.Patient;

internal class GetPatient(IPatientRepository patient, IPatientDtoMapperFactory mapper) : Endpoint<GetPatientByIdRequest, PatientDto>
{
    // I personally think its ok to skip the service if there is no business logic.
    readonly IPatientRepository _patient = patient;
    readonly IPatientDtoMapperFactory _mapper = mapper;

    // We use Configure() instead of Attributes because we want to do custom swagger docs.
    public override void Configure()
    {
        Get($"{ApiPath.Base}/{{id}}");
        AllowAnonymous();
        Description(b => b.Produces(204));
    }


    public override async Task HandleAsync(GetPatientByIdRequest req, CancellationToken ct)
    {
        Patient? patient = await _patient.GetByIdAsync(req.Id);
        if (patient != null)
        {
            await SendAsync(_mapper.Get().Map<PatientDto>(patient));
        }
        else
        {
            await SendNoContentAsync();
        }
    }
}


