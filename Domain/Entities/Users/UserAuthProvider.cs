namespace Domain.Entities.Users;

public class UserAuthProvider
{
    public int Id { get; private set; }
    public int UserId { get; private set; }
    public string Provider { get; private set; } = string.Empty;
    public string ProviderUserId { get; private set; } = string.Empty;

    // Navigation properties

    public virtual User? User { get; set; }

    public static UserAuthProvider New(string provider, string providerUserId)
    {
        return new UserAuthProvider()
        {
            Provider = provider,
            ProviderUserId = providerUserId
        };
    }
}