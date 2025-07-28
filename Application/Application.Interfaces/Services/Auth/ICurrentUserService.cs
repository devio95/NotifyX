using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Auth;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Username { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    string? GetClaim(string claimType);
}