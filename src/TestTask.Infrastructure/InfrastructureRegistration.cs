using System.Reflection;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Core.Common;
using TestTask.Infrastructure.Common;
using TestTask.Infrastructure.Exceptions;
using TestTask.Infrastructure.Processing;
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
                //factoryConfigurator.ConfigureEndpoints(registration);
                //factoryConfigurator.AutoStart = true;

                //factoryConfigurator.Message<AuthorCreated>(messageConfigurator => messageConfigurator.SetEntityName(nameof(AuthorCreated)));
                //factoryConfigurator.Publish<AuthorCreated>();

                //factoryConfigurator.Message<BockCreated>(messageConfigurator => messageConfigurator.SetEntityName(nameof(BockCreated)));
                //factoryConfigurator.Publish<BockCreated>();
            });

            configurator.AddEntityFrameworkOutbox<CalculationsDbContext>(o =>
            {
                o.UsePostgres();
                o.UseBusOutbox();
            });
        });
        
        
        // repositories
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        //services.AddScoped<IUsersRepository, UsersRepository>();

        // exceptions
        services.AddSingleton<IExceptionToResponseMapper, ExceptionToResponseMapper>();
        services.AddSingleton<IExceptionToResponseDeveloperMapper, ExceptionToResponseDeveloperMapper>();
        services.AddTransient<ExceptionHandlerDeveloperMiddleware>();
        services.AddTransient<ExceptionHandlerMiddleware>();

        // processing
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
    }
}