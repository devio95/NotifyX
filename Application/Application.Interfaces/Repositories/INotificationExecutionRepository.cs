using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface INotificationExecutionRepository
{
    Task<NotificationExecution> AddAsync(NotificationExecution execution);
}