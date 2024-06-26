﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace PayYourChart.Module.Patient;

public static partial class PatientModuleExtensions
{
    public static void AddPatientModule(this IServiceCollection services, IList<Assembly> mediatrAssemblies) 
    {
        mediatrAssemblies.Add(typeof(PatientModuleExtensions).Assembly);
        
        services.AddDbContext<EfPatientContext>();
        services.AddScoped<IPatientRepository, EfPatientRepository>();
        services.AddScoped<IPatientService, PatientService>();
        services.AddScoped<IBillRepository, EfBillRepository>();
        services.AddSingleton<IPatientDtoMapper, PatientDtoMapper>();
    }
}
