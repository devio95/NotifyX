using Application.Interfaces;
using Domain.Entities;

namespace Application.EntityServices.NotificationExecutions.Commands;

public class NotificationExecutionSimpleCommands(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task StartProcessingAsync(long notificationExecutionId)
    {
        await _unitOfWork.BeginTransactionAsync();
        NotificationExecution? notificationExecution = await _unitOfWork.NotificationExecutions.GetOneByIdAsync(notificationExecutionId);
        if (notificationExecution == null)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return;
        }

        notificationExecution.StartProcessing();

        _unitOfWork.NotificationExecutions.Update(notificationExecution);

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
    }
}