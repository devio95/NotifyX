using Keycloak.Options;

namespace WebAPI;

public static class DependencyInjection
{
    public static void AddDI(this IServiceCollection services, IConfiguration configuration)
    {
        AddOptions(services, configuration);
    }

    private static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<KeycloakOptions>()
            .Bind(configuration.GetSection(KeycloakOptions.Name))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}