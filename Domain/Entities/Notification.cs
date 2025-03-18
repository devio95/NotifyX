using Domain.Entities.Users;
using Domain.Enums;

namespace Domain.Entities;

public class Notification
{
    public required int Id { get; set; }
    public required int NotificationId { get; set; }
    public required int UserId { get; set; }
    public required string Subject { get; set; }
    public required string Text { get; set; }
    public required NotificationMethod Method { get; set; }
    public required NotificationType Type { get; set; }

    // Nagivation properties

    public virtual User? User { get; set; }
}