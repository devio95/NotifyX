using Domain.Entities.Users;

namespace Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByMailAsync(string email);
    Task AddAsync(User user);
}