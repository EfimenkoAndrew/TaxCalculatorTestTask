using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Persistence.CalculationsDb;

public class CalculationsDbContext(DbContextOptions<CalculationsDbContext> options) :  DbContext(options)
{
    internal const string Schema = "calculations";
    internal const string MigrationsHistoryTable = "__CalculationsMigrationsHistory";
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CalculationsDbContext).Assembly);
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
    }
}