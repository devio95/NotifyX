using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Functionalities.NotificationExecutions.Commands;

public class NotificationExecutionSendCommand : IRequest<Unit>
{
    public long NotificationExecutionId { get; }

    public NotificationExecutionSendCommand(long notificationExecutionId)
    {
        if (notificationExecutionId < 0)
        {
            throw new DataValidationException("NotificationId must be greater than 0");
        }

        NotificationExecutionId = notificationExecutionId;
    }
}

public class NotificationProcessCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<NotificationExecutionSendCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Unit> Handle(NotificationExecutionSendCommand request, CancellationToken cancellationToken)
    {

        try
        {
            NotificationExecution? notificationExecution = await _unitOfWork.NotificationExecutions.GetOneByIdAsync(request.NotificationExecutionId);
            if (notificationExecution == null)
            {
                return Unit.Value;
            }

            if (notificationExecution.EndDate != null)
            {
                return Unit.Value;
            }

            if (notificationExecution.Notification == null)
            {
                return Unit.Value;
            }

            await _unitOfWork.BeginTransactionAsync();

            // Send - jeszcze nie ma

            notificationExecution.FinishOk();
            NotificationExecution? nextNotificationExecution = notificationExecution.Notification.ScheduleNextNotificationExecution();
            notificationExecution.Notification.SetNextNotificationExecution(nextNotificationExecution);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return Unit.Value;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return Unit.Value;
        }
    }
}