namespace Application.Interfaces.Services.Messages;

public interface IMessageSubscriber
{
    Task StartAsync(Func<string, Task> handler);
    Task StopAsync();
}