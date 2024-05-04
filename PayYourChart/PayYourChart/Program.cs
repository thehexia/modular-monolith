using FastEndpoints;
using FastEndpoints.Swagger;
using PayYourChart.Module.Patient;
using PayYourChart.Module.Item;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Module registrations (you have to register the module for fastendpoints to automatically pick up any endpoints)
builder.Services.AddPatientModule();
builder.Services.AddItemModule();

// Fast endpoints registration
builder.Services
    .AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

app
    .UseDefaultExceptionHandler()
    .UseFastEndpoints()
    .UseSwaggerGen();


app.UseHttpsRedirection();

app.Run();
