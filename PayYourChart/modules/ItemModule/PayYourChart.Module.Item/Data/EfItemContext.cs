using Microsoft.EntityFrameworkCore;

namespace PayYourChart.Module.Item;

internal class EfItemContext : DbContext
{
    public DbSet<Item> Item { get; init; }

    public string DbPath { get; }

    /// <summary>
    /// This context is going to use SQL Lite for ease of example
    /// https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli
    /// </summary>
    public EfItemContext() 
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        // Notice that I sue the same database, but because these
        // are different modules, I could potentially use a totally
        // different one.
        //
        // One key concept of modular monoliths is
        // data isolation. Modules should not know about other
        // modules tables and there should be no enforced relationships
        // between tables that each module owns. If there are bridge tables
        // with foreign keys, that's a good indication your modules are too
        // small and should be combined.
        DbPath = Path.Join(path, "PayYourChart.db"); 
    }


    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    
    // Some code first things
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Item>()
            .HasIndex(i => i.ItemCode)
            .IsUnique();

        #region Seed
        builder.Entity<Item>().HasData(new Item() 
        {
            Id = 1,
            ItemCode = "92507",
            Price = 500.25m,
            Description = "Speech/hearing therapy"
        });

        builder.Entity<Item>().HasData(new Item() 
        {
            Id = 2,
            ItemCode = "92609",
            Price = 205.75m,
            Description = "Use of speech device service"
        });

        builder.Entity<Item>().HasData(new Item() 
        {
            Id = 3,
            ItemCode = "93797",
            Price = 12345.67m,
            Description = "Cardiac rehab"
        });

        builder.Entity<Item>().HasData(new Item() 
        {
            Id = 4,
            ItemCode = "93798",
            Price = 789.10m,
            Description = "Cardiac rehab/monitor"
        });
        #endregion
    }
}
