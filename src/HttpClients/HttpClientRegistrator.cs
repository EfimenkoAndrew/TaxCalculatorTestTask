using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

namespace HttpClients;

internal static class HttpClientRegistrator
{
    public static void RegisterHttpClient<TInterface, TImplementation>(
        IServiceCollection services,
        SystemHttpClientData systemHttpClientData,
        string name,
        Func<HttpClient, TImplementation> func)
        where TInterface : class
        where TImplementation : TInterface
    {
        if (systemHttpClientData == null) throw new ArgumentNullException(nameof(systemHttpClientData));

        if (string.IsNullOrEmpty(systemHttpClientData.Url)) throw new ArgumentException(nameof(systemHttpClientData.Url));

        services.AddHttpClient(name, httpClient =>
        {
            httpClient.BaseAddress = new Uri(systemHttpClientData.Url);
        });

        services.AddTransient<TInterface>(provider =>
        {
            var httpClientFactory = provider.GetRequiredService<IHttpClientFactory>();
            var httpContextAccessor = provider.GetRequiredService<IHttpContextAccessor>();
            var httpClient = httpClientFactory.CreateClient(name);

            if (httpContextAccessor.HttpContext != null)
            {
                var bearerToken = httpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString();
                if (!string.IsNullOrEmpty(bearerToken)) httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, bearerToken);
            }
            return func(httpClient);
        });
    }
}
