using Microsoft.EntityFrameworkCore;

namespace PayYourChart.Module.Common;

public interface IEfRepositoryBase : IDisposable
{
    void SaveChanges();
    Task SaveChangesAsync(CancellationToken ct = default);
}


public abstract class EfRepositoryBase<T> : IEfRepositoryBase where T : DbContext
{
    protected readonly T _context;

    public EfRepositoryBase(T context) 
    {
        _context = context;
    }

    public void SaveChanges() 
    {
        _context.SaveChanges();
    }

    public async Task SaveChangesAsync(CancellationToken ct = default) 
    {
        await _context.SaveChangesAsync(ct);
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
