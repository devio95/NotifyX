using Domain.Entities;

namespace Application.Interfaces.Repositories;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<IEnumerable<Notification>> GetAllWithoutNextNotificationAsync();
    Task<Notification?> GetByIdAsync(int id);
    void Update(Notification notification);
}