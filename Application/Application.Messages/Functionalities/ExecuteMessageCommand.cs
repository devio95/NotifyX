using Application.Interfaces;
using Application.Interfaces.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.Messages.Functionalities;

public class ExecuteMessageCommand : IRequest<Unit>
{
    public long NotificationExecutionId { get; }

    public ExecuteMessageCommand(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new DataValidationException("NotificationId must be greater than 0");
        }

        if (int.TryParse(message, out int notificationExecutionId) == false)
        {
            throw new DataValidationException("Message must be integer");
        }

        NotificationExecutionId = notificationExecutionId;
    }
}

public class ExecuteMessageCommandHandler(IUnitOfWork unitOfWork, ILoggingManager<ExecuteMessageCommandHandler> logger)
    : IRequestHandler<ExecuteMessageCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggingManager<ExecuteMessageCommandHandler> _logger = logger;
    public async Task<Unit> Handle(ExecuteMessageCommand request, CancellationToken cancellationToken)
    {

        try
        {
            _logger.LogInformation($"Executing Notification [{request.NotificationExecutionId}]");
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
            _logger.LogException(ex);
            await _unitOfWork.RollbackTransactionAsync();
            return Unit.Value;
        }
    }
}