﻿using AutoMapper;
using FastEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace PayYourChart.Module.Patient;

// If your request object isn't going to be used by multiple 
// endpoints, its ok to just declare it in the same file as the endpoint.
internal record class CreatePatientRequest(string FirstName, string LastName, string SSN, DateTime? DateOfBirth);
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


[HttpPost(ApiPath.Base)]
[AllowAnonymous]
internal class CreatePatient(IPatientService service) : Endpoint<CreatePatientRequest, PatientDto>
{
    readonly IPatientService _service = service;
    readonly Mapper mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<Patient, PatientDto>()));

    public override async Task HandleAsync(CreatePatientRequest req, CancellationToken ct)
    {
        Patient patient = await _service.AddPatientAsync(req.FirstName, req.LastName, req.SSN, req.DateOfBirth);
        await SendAsync(mapper.Map<PatientDto>(patient));
    }
}
