using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<IEnumerable<NotificationExecution>> GetAsync(DateTime dateTime, int pastMinutes)
    {
        pastMinutes = Math.Abs(pastMinutes) * -1;

        DateTime lowerLimit = dateTime.AddMinutes(pastMinutes);

        return await _context.NotificationExecutions
            .Where(x => x.IsProcessing == false)
            .Where(x => x.ExecutionDate >= lowerLimit)
            .Where(x => x.ExecutionDate <= dateTime)
            .ToListAsync();
    }

    public async Task<NotificationExecution?> GetOneByIdAsync(long notificationExecutionId)
    {
        return await _context.NotificationExecutions.FirstOrDefaultAsync(x => x.Id == notificationExecutionId);
    }
}