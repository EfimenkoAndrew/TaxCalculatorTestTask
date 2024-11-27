using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Core.Common;
using TestTask.Core.Domain.Calculations.Common;
using TestTask.Infrastructure.Common;
using TestTask.Infrastructure.Domain.Calculations;
using TestTask.Infrastructure.Exceptions;
using TestTask.Infrastructure.Processing;
using TestTask.Infrastructure.SystemEvents;
using TestTask.Persistence.CalculationsDb;

namespace TestTask.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // mediatr
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddMassTransit(configurator =>
        {
            // add RabbitMq
            configurator.UsingRabbitMq((registration, factoryConfigurator) =>
            {
                factoryConfigurator.ConfigureEndpoints(registration);
                //factoryConfigurator.AutoStart = true;

                factoryConfigurator.Message<CalculationCreated>(messageConfigurator => messageConfigurator.SetEntityName(nameof(CalculationCreated)));
                factoryConfigurator.Publish<CalculationCreated>();
            });

            configurator.AddEntityFrameworkOutbox<CalculationsDbContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });
        });

        // repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICalculationsRepository, CalculationsRepository>();

        // exceptions
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddSingleton<IExceptionToResponseDeveloperMapper, ExceptionToResponseDeveloperMapper>();
        services.AddTransient<ExceptionHandlerDeveloperMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();

        // processing
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
    }
}
