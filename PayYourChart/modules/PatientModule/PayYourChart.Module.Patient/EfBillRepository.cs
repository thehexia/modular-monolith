using Microsoft.EntityFrameworkCore;
using PayYourChart.Module.Common;

namespace PayYourChart.Module.Patient;

internal interface IBillRepository
{
    Task<Bill> AddAsync(long patientId, DateTime dueDate, string provider);
    Task AddLineItemAsync(LineItem lineItem);
    Task UpdateLineItemAsync(LineItem lineItem);
}


internal class EfBillRepository(EfPatientContext context) : EfRepositoryBase<EfPatientContext>(context), IBillRepository
{
    public async Task<Bill> AddAsync(long patientId, DateTime dueDate, string provider)
    {
        Patient? patient = await _context.Patient.Where(p => p.Id == patientId).SingleOrDefaultAsync();
        if (patient != null)
        {
            Bill bill = new() 
            {
                DueDate = dueDate,
                Provider = provider,
            };

            patient.Bills.Add(bill);

            await _context.SaveChangesAsync();

            return bill;
        }

        throw new InvalidOperationException($"No patient with id {patientId} could be found.");
    }


    public async Task AddLineItemAsync(LineItem lineItem)
    {
        await _context.LineItem.AddAsync(lineItem);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateLineItemAsync(LineItem lineItem)
    {
        _context.LineItem.Update(lineItem);
        await _context.SaveChangesAsync();
    }
}