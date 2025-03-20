using Application.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class NotificationRepository : INotificationRepository
{
    private readonly NotifyXDbContext _context;

    public NotificationRepository(NotifyXDbContext context)
    {
        _context = context;
    }

    public async Task<Notification?> GetByIdAsync(int id)
    {
        return await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
    }

    public async Task<IEnumerable<Notification>> GetAllAsync()
    {
        return await _context.Notifications.ToListAsync();
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
    }

    public void Update(Notification notification)
    {
        _context.Notifications.Update(notification);
    }

    public async Task<IEnumerable<Notification>> GetAllWithoutNextNotificationAsync()
    {
        return await _context.Notifications
            .Where(x => x.NextNotificationExecutionId == null)
            .ToListAsync();
    }
}