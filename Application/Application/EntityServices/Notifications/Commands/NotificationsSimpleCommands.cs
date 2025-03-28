using Application.Interfaces;
using Domain.Entities;

namespace Application.EntityServices.Notifications.Commands;

public class NotificationsSimpleCommands(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task ClearNextNotificationExecutionAsync(int notificationId)
    {
        await _unitOfWork.BeginTransactionAsync();
        Notification? notification = await _unitOfWork.Notifications.GetByIdAsync(notificationId);
        if (notification == null)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return;
        }

        notification.NextNotificationExecutionId = null;
        notification.NextNotificationExecution = null;

        _unitOfWork.Notifications.Update(notification);

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
    }
}