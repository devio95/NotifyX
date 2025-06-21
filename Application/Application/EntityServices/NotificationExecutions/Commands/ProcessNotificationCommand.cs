using Application.Interfaces;
using Domain.Entities;

namespace Application.EntityServices.NotificationExecutions.Commands;

public class ProcessNotificationCommand(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task ProcessNotificationExecutionAsync(int notificationId)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            Notification? notification = await _unitOfWork.Notifications.GetByIdAsync(notificationId);
            if (notification == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }

            if (notification.NextNotificationExecutionId == null || notification.NextNotificationExecution == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }

            // Send - jeszcze nie ma

            notification.NextNotificationExecution.FinishOk();
            notification.ClearNextNotificationExecution();
            NotificationExecution? notificationExecution = notification.ScheduleNextNotificationExecution();
            if (notificationExecution != null)
            {
                await _unitOfWork.NotificationExecutions.AddAsync(notificationExecution);
            }

            notification.SetNextNotificationExecution(notificationExecution);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
        }
    }
}