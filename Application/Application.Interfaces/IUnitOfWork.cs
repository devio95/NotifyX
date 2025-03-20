using Application.Interfaces.Repositories;
using System.Data;

namespace Application.Interfaces;

public interface IUnitOfWork : IDisposable, IAsyncDisposable
{
    INotificationRepository Notifications { get; }
    INotificationExecutionRepository NotificationExecutions { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}