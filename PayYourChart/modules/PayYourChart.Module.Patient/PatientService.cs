namespace PayYourChart.Module.Patient;

internal interface IPatientService
{
    Task<long> AddPatient(string firstName, string lastName, DateTime? dateOfBirth);
}

internal class PatientService(IPatientRepository repository) : IPatientService
{
    readonly IPatientRepository _repository = repository;

    public Task<long> AddPatient(string firstName, string lastName, DateTime? dateOfBirth)
    {
        
    }
}
