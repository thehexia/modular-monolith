using FastEndpoints;
using FastEndpoints.Swagger;
using PayYourChart.Module.Patient;
using PayYourChart.Module.Item;
using System.Reflection;
using PayYourChart.CertificateAuth.Extensions;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Net;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddJsonConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Module registrations (you have to register the module for fastendpoints to automatically pick up any endpoints)\
List<Assembly> mediatrAssemblies = [typeof(Program).Assembly];
builder.Services.AddPatientModule(mediatrAssemblies);
builder.Services.AddItemModule(mediatrAssemblies);

// Setup MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(mediatrAssemblies.ToArray()));

// Fast endpoints registration
builder.Services
    .AddFastEndpoints()
    .SwaggerDocument();

// Add a time provider
builder.Services.AddSingleton(TimeProvider.System);

// Add in auth stuff
builder.Services.AddCertificateAuth();

// Tell kestrel we need https to do cert based auth.
var cert = new X509Certificate2("certs/server-cert.pfx");
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.ConfigureHttpsDefaults(options =>
        options.ClientCertificateMode = ClientCertificateMode.AllowCertificate);

    // Only do this if in local dev mode.
    options.Listen(IPAddress.Loopback, 5244); // Http
    options.Listen(IPAddress.Loopback, 5245, listenOptions =>
    {
        // In real code use key vault or something like that.
        listenOptions.UseHttps("certs/server-cert.pfx");
    });
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app
    .UseDefaultExceptionHandler()
    .UseFastEndpoints()
    .UseSwaggerGen();


app.UseHttpsRedirection();

app.Run();
