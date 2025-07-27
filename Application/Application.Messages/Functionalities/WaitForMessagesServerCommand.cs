using Application.Interfaces.Services.Messages;
using MediatR;

namespace Application.Messages.Functionalities;

public record WaitForMessagesServerCommand : IRequest<bool>;

public class WaitForMessagesServerCommandHandler(IMessagePublisher publisher)
    : IRequestHandler<WaitForMessagesServerCommand, bool>
{
    private readonly IMessagePublisher _publisher = publisher;

    public async Task<bool> Handle(WaitForMessagesServerCommand request, CancellationToken cancellationToken)
    {
        for (int i = 0; i < 30; i++)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }

            if (await _publisher.PingAsync())
            {
                return true;
            }

            await Task.Delay(500);
        }

        return false;
    }
}