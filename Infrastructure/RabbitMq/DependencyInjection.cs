using Microsoft.Extensions.DependencyInjection;

namespace RabbitMq;

public static class DependencyInjection
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services)
    {
        return services
            .AddScoped<IPublisher, Publisher>()
            .AddScoped<ISubscriber, Subscriber>();
    }
}