using Application.Interfaces;
using Application.Interfaces.Services.Auth;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddScoped<ITokenService, TokenService>()
            .AddScoped(typeof(ILoggingManager<>), typeof(LoggingManager<>));
    }
}
