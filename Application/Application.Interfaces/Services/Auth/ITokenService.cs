using Domain.Models;
using System.Security.Claims;

namespace Application.Interfaces.Services.Auth;

public interface ITokenService
{
    Token CreateToken(List<Claim> claims);
}