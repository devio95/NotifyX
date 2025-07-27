using Application;
using Application.Auth;
using Application.Messages;
using Database;
using Keycloak;
using Keycloak.Options;
using RabbitMq;

namespace WebAPI;

public static class DependencyInjection
{
    public static void AddDI(this IServiceCollection services, IConfiguration configuration)
    {
        AddOptions(services, configuration);
        services.AddApplication();
        services.AddApplicationAuth();
        services.AddApplicationMessages();
        services.AddDatabase();
        services.AddRepositories();
        services.AddKeycloak(configuration);
        services.AddRabbitMqSingleton();
    }

    private static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<KeycloakOptions>()
            .Bind(configuration.GetSection(KeycloakOptions.Name))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}