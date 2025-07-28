namespace Application.DTO.Users;

public class UserCreateDto
{
    public int Id { get; }
    public string Email { get; } = string.Empty;
    public string Name { get; } = string.Empty;

    public UserCreateDto(int id, string email, string name)
    {
        Id = id;
        Email = email;
        Name = name;
    }
}