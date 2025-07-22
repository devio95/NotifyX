using Application.Interfaces.Services.Auth;
using Keycloak.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Keycloak;

public static class DependencyInjection
{
    public static void AddKeycloak(this IServiceCollection services, IConfiguration configuration)
    {
        KeycloakOptions options = new KeycloakOptions();
        configuration.GetSection(KeycloakOptions.Name).Bind(options);

        services.AddHttpClient(KeycloakOptions.HttpClientName, client =>
        {
            client.BaseAddress = new Uri(options.Address);
        });

        services.AddScoped<IAuthService, KeycloakAuthService>();
    }
}