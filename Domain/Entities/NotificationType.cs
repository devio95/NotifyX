namespace Domain.Entities;

public class NotificationType
{
    public required NotificationTypeEnum Id { get; set; }
    public required string Name { get; set; }
}