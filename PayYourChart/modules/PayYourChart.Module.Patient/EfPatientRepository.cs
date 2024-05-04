using Microsoft.EntityFrameworkCore;
using PayYourChart.Module.Common;

namespace PayYourChart.Module.Patient;

internal interface IPatientRepository : IEfRepositoryBase
{
    Task<Patient> AddAsync(Patient patient);
    Task<Patient?> GetByIdAsync(long id);
}


internal class EfPatientRepository(EfPatientContext context) : EfRepositoryBase<EfPatientContext>(context), IPatientRepository
{
    public async Task<Patient> AddAsync(Patient patient)
    {
        await _context.Patient.AddAsync(patient);
        return patient;
    }

    public async Task<Patient?> GetByIdAsync(long id) 
    {
        return await _context.Patient.Where(p => p.Id == id).SingleOrDefaultAsync();
    }
}
