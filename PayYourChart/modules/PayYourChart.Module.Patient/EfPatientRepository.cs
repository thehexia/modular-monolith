using PayYourChart.Module.Common;

namespace PayYourChart.Module.Patient;

internal interface IPatientRepository : IEfRepositoryBase
{
    Task<Patient> AddAsync(Patient patient);
}


internal class EfPatientRepository(EfPatientContext context) : EfRepositoryBase<EfPatientContext>(context), IPatientRepository
{
    public async Task<Patient> AddAsync(Patient patient)
    {
        await _context.Patient.AddAsync(patient);
        return patient;
    }
}
