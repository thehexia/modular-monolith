using FastEndpoints;
using FastEndpoints.Swagger;
using PayYourChart.Module.Patient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Module registrations (you have to register the module for fastendpoints to automatically pick up any endpoints)
builder.Services.AddPatientModule();

// Fast endpoints registration
builder.Services.AddFastEndpoints()
    .SwaggerDocument();

var app = builder.Build();

app.UseFastEndpoints()
    .UseSwaggerGen();


app.UseHttpsRedirection();

app.Run();
