namespace PayYourChart.Module.Patient;

internal interface IPatientService
{
    Task<Patient> AddPatientAsync(string firstName, string lastName, DateTime? dateOfBirth);
}


internal class PatientService(IPatientRepository repository) : IPatientService
{
    readonly IPatientRepository _repository = repository;

    public async Task<Patient> AddPatientAsync(string firstName, string lastName, DateTime? dateOfBirth)
    {
        Patient patient = await _repository.AddAsync(new()
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth
        });

        await _repository.SaveChangesAsync();

        return patient;
    }
}
