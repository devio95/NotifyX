namespace Domain.Entities.Users;

public class User
{
    public int Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }

    // Navigation properties

    public virtual UserAuthProvider? AuthProvider { get; set; }
    public virtual ICollection<Notification> Notifications { get; set; } = [];
    public virtual ICollection<Role> Roles { get; set; } = [];

    public static User New(string email, string name, string provider, string providerUserId)
    {
        return new User()
        {
            Email = email,
            Name = name,
            CreatedAt = DateTime.UtcNow,
            AuthProvider = UserAuthProvider.New(provider, providerUserId)
        };
    }

    public static User Admin()
    {
        return new User
        {
            Id = 1,
            Email = "",
            Name = "Admin",
            CreatedAt = new DateTime(2025, 3, 19, 0, 0, 0, DateTimeKind.Utc)
        };
    }
}