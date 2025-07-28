using Domain.Entities.Users;
using System.Security.Claims;

namespace Application.Services;

public interface IClaimsService
{
    List<Claim> GetClaims(User user);
}

internal class ClaimsService : IClaimsService
{
    public List<Claim> GetClaims(User user)
    {
        List<Claim> toReturn = new List<Claim>()
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name)
        };

        foreach (Role role in user.Roles)
        {
            toReturn.Add(new(ClaimTypes.Role, role.Name));
            toReturn.Add(new("role", role.Name));
        }

        return toReturn;
    }
}