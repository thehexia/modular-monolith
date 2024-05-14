using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PayYourChart.Module.Tests.Common;

namespace PayYourChart.Module.Patient.UnitTests;

[DisableWafCache]
public class PatientsEndpointsTestFixture : BaseTestApp
{
    public readonly Fixture f = new();
    internal readonly IPatientRepository PatientRepository;
    internal readonly IBillRepository BillRepository;
    internal readonly IPatientService PatientService;
    internal readonly IMediator Mediator;
    internal readonly IPatientDtoMapper Mapper;

    public PatientsEndpointsTestFixture()
    {
        PatientRepository = Substitute.For<IPatientRepository>();
        BillRepository = Substitute.For<IBillRepository>();
        PatientService = Substitute.For<IPatientService>();
        Mediator = Substitute.For<IMediator>();
        Mapper = new PatientDtoMapper();
        f.Register(() => PatientRepository);
        f.Register(() => BillRepository);
        f.Register(() => Mediator);
        f.Register(() => Mapper);
    }
    

    protected override void ConfigureServices(IServiceCollection services)
    {
        // do test service registration here
        services.AddScoped<IPatientRepository>((provider) => PatientRepository);
        services.AddScoped<IBillRepository>((provider) => BillRepository);
        services.AddScoped<IPatientService>((provider) => PatientService);
        services.AddScoped<IMediator>((provider) => Mediator);
    }
}