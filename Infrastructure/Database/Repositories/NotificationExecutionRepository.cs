using Application.Interfaces.Repositories;
using Domain.Entities;

namespace Database.Repositories;

public class NotificationExecutionRepository : INotificationExecutionRepository
{
    private readonly NotifyXDbContext _context;

    public NotificationExecutionRepository(NotifyXDbContext context)
    {
        _context = context;
    }

    public async Task<NotificationExecution> AddAsync(NotificationExecution execution)
    {
        await _context.NotificationExecutions.AddAsync(execution);
        return execution;
    }
}