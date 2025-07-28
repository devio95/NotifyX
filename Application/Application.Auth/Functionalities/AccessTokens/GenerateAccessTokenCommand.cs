using Application.Auth.DTO;
using Application.Interfaces;
using Application.Interfaces.Exceptions;
using Application.Interfaces.Services.Auth;
using Application.Services;
using Domain.Entities.Users;
using Domain.Models;
using MediatR;
using System.Net.Mail;
using System.Security.Claims;

namespace Application.Auth.AccessTokens.Commands;

public class GenerateAccessTokenCommand : IRequest<GenerateAccessTokenCommandResponse>
{
    public string Email { get; }
    public string Password { get; }

    public GenerateAccessTokenCommand(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new DataValidationException("Empty password");
        }

        try
        {
            var mail = new MailAddress(email);
        }
        catch (FormatException)
        {
            throw new DataValidationException("Wrong email");
        }

        Email = email;
        Password = password;
    }
}

public class GenerateAccessTokenCommandHandler(IExternalAuthService authService, IUnitOfWork unitOfWork,
    ITokenService tokenService, IClaimsService claimsService)
    : IRequestHandler<GenerateAccessTokenCommand, GenerateAccessTokenCommandResponse>
{
    private readonly IExternalAuthService _externalAuthService = authService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IClaimsService _claimsService = claimsService;

    public async Task<GenerateAccessTokenCommandResponse> Handle(GenerateAccessTokenCommand request, CancellationToken cancellationToken)
    {
        ExternalAuthResult authResult = await _externalAuthService.AuthenticateAsync(request.Email, request.Password);
        if (string.IsNullOrWhiteSpace(authResult.AccessToken))
        {
            throw new UnauthorizedAccessException();
        }

        User? user = await _unitOfWork.Users.GetByMailAsync(request.Email);
        if (user == null)
        {
            throw new Exception($"No user {request.Email} in database");
        }

        List<Claim> claims = _claimsService.GetClaims(user);
        Token token = _tokenService.CreateToken(claims);


        return new GenerateAccessTokenCommandResponse(new GenerateAccessTokenDto(
            accessToken: token.AccessToken,
            tokenType: token.Type,
            expiresIn: token.ExpiresIn,
            audience: token.Audience,
            issuer: token.Issuer));
    }
}