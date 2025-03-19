namespace Domain.Entities.Users;

public class Role
{
    public required RoleEnum Id { get; set; }
    public required string Name { get; set; }
    public required int DescriptionId { get; set; }
}