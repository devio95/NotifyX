using Application;
using Application.Auth;
using Database;
using Infrastructure;
using Infrastructure.Options;
using Keycloak;
using Keycloak.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI;

public static class DependencyInjection
{
    public static void AddDI(this IServiceCollection services, IConfiguration configuration)
    {
        AddOptions(services, configuration);
        services.AddApplication();
        services.AddApplicationAuth();
        services.AddDatabase();
        services.AddRepositories();
        services.AddKeycloak(configuration);
        services.AddInfrastructure();
    }

    private static void AddOptions(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<KeycloakOptions>()
            .Bind(configuration.GetSection(KeycloakOptions.Name))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<TokenOptions>()
            .Bind(configuration.GetSection(TokenOptions.Name))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}