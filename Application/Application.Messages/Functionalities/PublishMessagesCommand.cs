using Application.DTO.NotificationExecutions;
using Application.EntityServices.NotificationExecutions.Queries;
using Application.Functionalities.NotificationExecutions.Queries;
using Application.Interfaces;
using MediatR;

namespace Application.Messages.Functionalities;

public record PublishMessagesCommand : IRequest<Unit>;

public class PublishMessagesCommandHandler(IMediator mediator, ILoggingManager<PublishMessageCommandHandler> logger)
    : IRequestHandler<PublishMessagesCommand, Unit>
{
    private readonly IMediator _mediator = mediator;
    private readonly ILoggingManager<PublishMessageCommandHandler> _logger = logger;
    public async Task<Unit> Handle(PublishMessagesCommand request, CancellationToken cancellationToken)
    {
        NotificationExecutionsGetFilteredResponse notificationsToDispatch = await _mediator.Send(new NotificationExecutionsGetFilteredQuery(5));
        if (notificationsToDispatch.Response.Any() == false)
        {
            _logger.LogInformation("Nothing to dispatch");
            return Unit.Value;
        }

        foreach (NotificationExecutionsGetFilteredDto notification in notificationsToDispatch.Response)
        {
            await _mediator.Send(new PublishMessageCommand(notification.Id));
        }

        return Unit.Value;
    }
}
