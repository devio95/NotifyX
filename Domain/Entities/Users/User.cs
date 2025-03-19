namespace Domain.Entities.Users;

public class User
{
    public required int Id { get; set; }
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required DateTime CreatedAt { get; set; }

    // Navigation properties

    public virtual UserAuthProvider? AuthProvider { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; } = [];
    public virtual ICollection<Role> Roles { get; set; } = [];
}