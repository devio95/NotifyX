using Application.DTO.NotificationExecutions;
using Application.EntityServices.NotificationExecutions.Queries;
using Application.Functionalities.NotificationExecutions.Queries;
using Application.Interfaces;
using MediatR;

namespace Application.Messages.Functionalities;

public record PublishMessages : IRequest<Unit>;

public class PublishMessagesCommandHandler(IMediator mediator)
    : IRequestHandler<PublishMessages, Unit>
{
    private readonly IMediator _mediator = mediator;
    public async Task<Unit> Handle(PublishMessages request, CancellationToken cancellationToken)
    {
        NotificationExecutionsGetFilteredResponse notificationsToDispatch = await _mediator.Send(new NotificationExecutionsGetFilteredQuery(5));
        if (notificationsToDispatch.Response.Any() == false)
        {
            return Unit.Value;
        }

        foreach (NotificationExecutionsGetFilteredDto notification in notificationsToDispatch.Response)
        {
            await _mediator.Send(new PublishMessageCommand(notification.Id));
        }

        return Unit.Value;
    }
}
