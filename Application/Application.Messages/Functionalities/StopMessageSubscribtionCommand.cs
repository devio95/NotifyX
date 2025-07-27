using Application.Interfaces.Services.Messages;
using MediatR;

namespace Application.Messages.Functionalities;

public record StopMessageSubscribtionCommand : IRequest<Unit>;

public class StopMessageSubscribtionCommandHandler(IMessageSubscriber subscriber)
    : IRequestHandler<StopMessageSubscribtionCommand, Unit>
{
    private readonly IMessageSubscriber _subscriber = subscriber;

    public async Task<Unit> Handle(StopMessageSubscribtionCommand request, CancellationToken cancellationToken)
    {
        await _subscriber.StopAsync();
        return Unit.Value;
    }
}