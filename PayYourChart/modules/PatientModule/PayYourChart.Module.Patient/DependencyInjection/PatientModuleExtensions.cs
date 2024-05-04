using Microsoft.Extensions.DependencyInjection;

namespace PayYourChart.Module.Patient;

public static partial class PatientModuleExtensions
{
    public static void AddPatientModule(this IServiceCollection services) 
    {
        services.AddDbContext<EfPatientContext>();
        services.AddScoped<IPatientRepository, EfPatientRepository>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddSingleton<IPatientDtoMapper, PatientDtoMapper>();
    }
}
