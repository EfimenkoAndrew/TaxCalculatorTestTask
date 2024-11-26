using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TestTask.Application;

public static class ApplicationRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(ApplicationRegistration))!));
    }   
}