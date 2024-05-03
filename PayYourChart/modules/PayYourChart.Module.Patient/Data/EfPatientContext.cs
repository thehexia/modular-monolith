using Microsoft.EntityFrameworkCore;

namespace PayYourChart.Module.Patient;

internal class EfPatientContext : DbContext
{
    public DbSet<Patient> Patient { get; init; }

    public string DbPath { get; }

    /// <summary>
    /// This context is going to use SQL Lite for ease of example
    /// https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
    /// </summary>
    public EfPatientContext() 
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "PayYourChart.db");
    }


    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}
