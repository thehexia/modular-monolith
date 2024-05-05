using FastEndpoints;
using FastEndpoints.Swagger;
using PayYourChart.Module.Patient;
using PayYourChart.Module.Item;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app
    .UseDefaultExceptionHandler()
    .UseFastEndpoints()
    .UseSwaggerGen();


app.UseHttpsRedirection();

app.Run();
