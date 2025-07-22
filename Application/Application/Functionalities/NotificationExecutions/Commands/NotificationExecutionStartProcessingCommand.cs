using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Services;
using Domain.Entities;
using MediatR;

namespace Application.EntityServices.NotificationExecutions.Commands;

public class NotificationExecutionStartProcessingCommand : IRequest<Unit>
{
    public long NotificationExecutionId { get; }
    public NotificationExecutionStartProcessingCommand(long notificationExecutionId)
    {
        if (notificationExecutionId < 0)
        {
            throw new DataValidationException("NotificationId must be greater than 0");
        }

        NotificationExecutionId = notificationExecutionId;
    }
}

public class NotificationExecutionStartProcessingCommandHandler(IUnitOfWork unitOfWork, IMessagePublisher publisher)
    : IRequestHandler<NotificationExecutionStartProcessingCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMessagePublisher _publisher = publisher;

    public async Task<Unit> Handle(NotificationExecutionStartProcessingCommand request, CancellationToken cancellationToken)
    {
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