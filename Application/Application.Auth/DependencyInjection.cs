using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Auth;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationAuth(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}