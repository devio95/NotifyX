namespace Domain.Entities.Users;

public class UserAuthProvider
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required string Provider { get; set; }
    public required string ProviderUserId { get; set; }

    // Navigation properties

    public virtual User? User { get; set; }
}