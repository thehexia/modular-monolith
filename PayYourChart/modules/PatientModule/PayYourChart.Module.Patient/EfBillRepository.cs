using Microsoft.EntityFrameworkCore;
using PayYourChart.Module.Common;

namespace PayYourChart.Module.Patient;

internal interface IBillRepository : IEfRepositoryBase
{
    Task<Bill> AddAsync(long patientId, DateTime dueDate, string provider);
    Task<Bill?> GetAsync(long billId);
    Task AddLineItemAsync(LineItem lineItem);
    Task UpdateLineItemAsync(long lineItemId, DateTime dateOfService, string provider, short quantity);
    Task DeleteLineItemAsync(long lineItemId);
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

            return bill;
        }

        throw new InvalidOperationException($"No patient with id {patientId} could be found.");
    }

    public async Task<Bill?> GetAsync(long billId)
    {
        Bill? bill = await _context.Bill.Where(b => b.Id == billId).SingleOrDefaultAsync();

        if (bill != null)
        {
            bill.LineItems = await _context.LineItem.Where(l => l.BillId == billId).ToListAsync();
        }

        return bill;
    }

    public async Task AddLineItemAsync(LineItem lineItem)
    {
        await _context.LineItem.AddAsync(lineItem);
    }

    public async Task UpdateLineItemAsync(long lineItemId, DateTime dateOfService, string provider, short quantity)
    {
        await _context.LineItem
            .Where(l => l.Id == lineItemId)
            .ExecuteUpdateAsync(l => l
                .SetProperty(i => i.DateOfService, dateOfService)
                .SetProperty(i => i.Provider, provider)
                .SetProperty(i => i.Quantity, quantity));
    }

    
    public async Task DeleteLineItemAsync(long lineItemId)
    {
        await _context.LineItem
            .Where(l => l.Id == lineItemId)
            .ExecuteDeleteAsync();
    }
}