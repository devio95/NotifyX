using Application.DTO.NotificationExecutions;
using Application.EntityServices.NotificationExecutions.Queries;
using Application.Functionalities.NotificationExecutions.Queries;
using MediatR;

namespace Application.Messages.Functionalities;

public record ExecuteMessagesCommand : IRequest<Unit>;

public class ExecuteMessagesCommandHandler(IMediator mediator)
    : IRequestHandler<ExecuteMessagesCommand, Unit>
{
    private readonly IMediator _mediator = mediator;
    public async Task<Unit> Handle(ExecuteMessagesCommand request, CancellationToken cancellationToken)
    {
        NotificationExecutionsGetFilteredResponse notificationsToDispatch = await _mediator.Send(new NotificationExecutionsGetFilteredQuery(5));
        if (notificationsToDispatch.Response.Any() == false)
        {
            LogNotificationsToDispatch();
            return Unit.Value;
        }

        foreach (NotificationExecutionsGetFilteredDto notification in notificationsToDispatch.Response)
        {
            await _mediator.Send(new PublishMessageCommand(notification.Id));
        }

        return Unit.Value;
    }

    private void LogNotificationsToDispatch()
    {
        // TODO
    }
}
