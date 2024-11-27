using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.Persistence.CalculationsDb;

public static class CalculationsDbRegistration
{
    private const string ConnectionStringName = "CalculationDb";

    public static void AddToDoListDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringName)
                               ?? throw new AggregateException(
                                   $"Connection string: '{ConnectionStringName}' is not found in configurations.");

        services.AddDbContext<CalculationsDbContext>(options =>
        {
            options.UseNpgsql(
                connectionString,
                npgsqlOptions =>
                {
                    npgsqlOptions.MigrationsHistoryTable(
                        CalculationsDbContext.MigrationsHistoryTable,
                        CalculationsDbContext.Schema);
                });
        });

        services.AddScoped<DbContext>(provider => provider.GetRequiredService<CalculationsDbContext>());
    }
}
