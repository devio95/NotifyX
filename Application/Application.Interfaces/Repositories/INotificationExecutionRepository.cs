using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface INotificationExecutionRepository
{
    Task<NotificationExecution> AddAsync(NotificationExecution execution);
    Task<IEnumerable<NotificationExecution>> GetAsync(DateTime dateTime, int pastMinutes);
    Task<IEnumerable<NotificationExecution>> GetAll();
    Task<NotificationExecution?> GetOneByIdAsync(long notificationExecutionId);
    void Update(NotificationExecution notification);
}