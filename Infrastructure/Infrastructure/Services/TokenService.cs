using Application.Interfaces.Services.Auth;
using Domain.Models;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Services;

public class TokenService(IOptions<TokenOptions> tokenOptions)
    : ITokenService
{
    private readonly TokenOptions _tokenOptions = tokenOptions.Value;
    public Token CreateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        DateTime expiresIn = DateTime.UtcNow.AddSeconds(_tokenOptions.ExpiresIn);

        var token = new JwtSecurityToken(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            expires: expiresIn,
            signingCredentials: credentials);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return new Token(
            accessToken: tokenValue,
            type: "Bearer",
            expiresIn: expiresIn,
            audience: _tokenOptions.Audience,
            issuer: _tokenOptions.Issuer);
    }
}