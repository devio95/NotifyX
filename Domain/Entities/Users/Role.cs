namespace Domain.Entities.Users;

public class Role
{
    public required Roles Id { get; set; }
    public required string Name { get; set; }
    public required int DescriptionId { get; set; }

    // Navigation properties
    public virtual ICollection<User> Users { get; set; } = [];
}