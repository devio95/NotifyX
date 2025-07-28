using Application.Interfaces.Repositories;
using Domain.Entities.Users;

namespace Database.Repositories;

public class UserAuthProviderRepository : IUserAuthProviderRepository
{
    private readonly NotifyXDbContext _context;

    public UserAuthProviderRepository(NotifyXDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserAuthProvider userAuthProvider)
    {
        await _context.UserAuthProviders.AddAsync(userAuthProvider);
    }
}