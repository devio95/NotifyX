using Application.Interfaces;
using Application.Interfaces.Services.Messages;
using MediatR;

namespace Application.Messages.Functionalities;

public record StartMessageSubscribtionCommand : IRequest<Unit>;

public class StartMessageSubscribtionCommandHandler(IMessageSubscriber subscriber, IMediator mediator, ILoggingManager<StartMessageSubscribtionCommand> logger)
    : IRequestHandler<StartMessageSubscribtionCommand, Unit>
{
    private readonly IMessageSubscriber _subscriber = subscriber;
    private readonly IMediator _mediator = mediator;
    private readonly ILoggingManager<StartMessageSubscribtionCommand> _logger = logger;

    public async Task<Unit> Handle(StartMessageSubscribtionCommand request, CancellationToken cancellationToken)
    {
        await _subscriber.StartAsync(ProcessMessageAsync);
        return Unit.Value;
    }

    private async Task ProcessMessageAsync(string message)
    {
        _logger.LogInformation($"Message to process : [{message}]");
        await _mediator.Send(new ExecuteMessageCommand(message));
    }
}