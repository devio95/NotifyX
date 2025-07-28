using Domain.Entities.Users;

namespace Application.Interfaces.Repositories;

public interface IUserAuthProviderRepository
{
    Task AddAsync(UserAuthProvider userAuthProvider);
}