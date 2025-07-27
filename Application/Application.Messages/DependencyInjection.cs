using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Messages;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationMessages(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;
    }
}