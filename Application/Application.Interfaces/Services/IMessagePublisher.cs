namespace Application.Interfaces.Services;

public interface IMessagePublisher
{
    Task SendAsync(object obj);
}