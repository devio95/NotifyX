using Database;

namespace WebApp;

internal static class DependencyInjection
{
    internal static IServiceCollection AddDI(this IServiceCollection services)
    {
        return services.AddDatabase();
    }
}