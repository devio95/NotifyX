using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddScoped(typeof(ILoggingManager<>), typeof(LoggingManager<>));
    }
}
