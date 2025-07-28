using Application.DTO.Users;
using Application.Functionalities.Users.Commands.UserCreate;
using Application.Interfaces;
using Application.Interfaces.Exceptions;
using Domain.Entities.Users;
using MediatR;
using System.Net.Mail;

namespace Application.Functionalities.Users.Commands.CreateUser;

public class UserCreateCommand : IRequest<UserCreateCommandResponse>
{
    public string Email { get; }
    public string Name { get; }
    public string Provider { get; }
    public string ProviderUserId { get; }

    public UserCreateCommand(string email, string name, string provider, string providerUserId)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(providerUserId))
        {
            throw new DataValidationException();
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
        Name = name;
        Provider = provider;
        ProviderUserId = providerUserId;
    }
}

public class UserCreateCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UserCreateCommand, UserCreateCommandResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<UserCreateCommandResponse> Handle(UserCreateCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        User user = User.New(request.Email, request.Name, request.Provider, request.ProviderUserId);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return new UserCreateCommandResponse(new UserCreateDto(
            id: user.Id,
            email: user.Email,
            name: user.Name);
    }
}