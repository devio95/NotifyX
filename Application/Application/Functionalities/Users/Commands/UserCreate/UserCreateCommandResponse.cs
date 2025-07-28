using Application.DTO.Users;

namespace Application.Functionalities.Users.Commands.UserCreate;

public class UserCreateCommandResponse
{
    public UserCreateDto Dto { get; }

    public UserCreateCommandResponse(UserCreateDto dto)
    {
        Dto = dto;
    }
}