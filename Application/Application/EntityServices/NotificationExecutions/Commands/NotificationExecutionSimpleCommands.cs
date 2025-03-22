using Application.Interfaces;
using Domain.Entities;

namespace Application.EntityServices.NotificationExecutions.Commands;

public class NotificationExecutionSimpleCommands(IUnitOfWork unitOfWork)
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task FinishOkAsync(long notificationExecutionId)
    {
        await _unitOfWork.BeginTransactionAsync();
        NotificationExecution? notificationExecution = await _unitOfWork.NotificationExecutions.GetOneByIdAsync(notificationExecutionId);
        if (notificationExecution == null)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return;
        }

        notificationExecution.FinishOk();
        _unitOfWork.NotificationExecutions.Equals(notificationExecution);

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
    }

    public async Task FinishNokAsync(long notificationExecutionId, string error)
    {
        await _unitOfWork.BeginTransactionAsync();
        NotificationExecution? notificationExecution = await _unitOfWork.NotificationExecutions.GetOneByIdAsync(notificationExecutionId);
        if (notificationExecution == null)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return;
        }

        notificationExecution.FinishNok(error);

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
    }

    public async Task SetIsProcessedAsync(long notificationExecutionId)
    {
        await _unitOfWork.BeginTransactionAsync();
        NotificationExecution? notificationExecution = await _unitOfWork.NotificationExecutions.GetOneByIdAsync(notificationExecutionId);
        if (notificationExecution == null)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return;
        }

        notificationExecution.IsProcessing = true;

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
    }
}