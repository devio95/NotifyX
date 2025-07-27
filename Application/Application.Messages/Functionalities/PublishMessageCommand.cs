using Application.Interfaces;
using Application.Interfaces.Exceptions;
using Application.Interfaces.Services.Messages;
using Domain.Entities;
using MediatR;

namespace Application.Messages.Functionalities;

public class PublishMessageCommand : IRequest<Unit>
{
    public long NotificationExecutionId { get; }
    public PublishMessageCommand(long notificationExecutionId)
    {
        if (notificationExecutionId < 0)
        {
            throw new DataValidationException("NotificationId must be greater than 0");
        }

        NotificationExecutionId = notificationExecutionId;
    }
}

public class PublishMessageCommandHandler(IUnitOfWork unitOfWork, IMessagePublisher publisher, ILoggingManager<PublishMessageCommandHandler> logger)
    : IRequestHandler<PublishMessageCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessagePublisher _publisher = publisher;
    private readonly ILoggingManager<PublishMessageCommandHandler> _logger = logger;

    public async Task<Unit> Handle(PublishMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Publishing notification [{request.NotificationExecutionId}]");
        await _unitOfWork.BeginTransactionAsync();
        NotificationExecution? notificationExecution = await _unitOfWork.NotificationExecutions.GetOneByIdAsync(request.NotificationExecutionId);
        if (notificationExecution == null)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Unit.Value;
        }

        notificationExecution.SetIsProcessing();

        _unitOfWork.NotificationExecutions.Update(notificationExecution);

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();

        await _publisher.SendAsync(notificationExecution.Id);

        return Unit.Value;
    }
}