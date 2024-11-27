using Microsoft.Extensions.DependencyInjection;
using TestTask.Api.HttpClients;

namespace HttpClients;

public static class ServiceCollectionExtensions
{
    private const string DefaultHttpClientName = "Calculations.HttpClient";

    // Register department http client
    public static void RegisterCalculationsHttpClient(this IServiceCollection services, SystemHttpClientData systemHttpClientData)
    {
        services.RegisterHttpClient<ICalculationsHttpClient, CalculationsHttpClient>(
            systemHttpClientData,
            systemHttpClientData.HttpClientName ?? DefaultHttpClientName,
            httpClient => new CalculationsHttpClient(httpClient));
    }

    public static void RegisterHttpClient<TInterface, TImplementation>(
        this IServiceCollection services,
        SystemHttpClientData systemHttpClientData,
        string name,
        Func<HttpClient, TImplementation> func)
        where TInterface : class
        where TImplementation : TInterface
    {
        HttpClientRegistrator.RegisterHttpClient<TInterface, TImplementation>(services, systemHttpClientData, name, func);
    }
}
