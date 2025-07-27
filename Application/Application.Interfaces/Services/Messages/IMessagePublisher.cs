namespace Application.Interfaces.Services.Messages;

public interface IMessagePublisher
{
    Task SendAsync(object obj);
}