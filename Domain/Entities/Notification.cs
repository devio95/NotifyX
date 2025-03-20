using Domain.Entities.Users;
using Domain.Enums;

namespace Domain.Entities;

public class Notification
{
    public required int Id { get; set; }
    public required int UserId { get; set; }
    public required string Subject { get; set; }
    public required string Text { get; set; }
    public required NotificationMethodEnum NotificationMethodId { get; set; }
    public required NotificationTypeEnum NotificationTypeId { get; set; }
    public required long? NextNotificationExecutionId { get; set; }
    public required DateTime? EndDate { get; set; }
    public required DateTime ExecutionStart { get; set; }

    // Nagivation properties

    public virtual NotificationMethod? NotificationMethod { get; set; }
    public virtual NotificationType? NotificationType { get; set; }
    public virtual User? User { get; set; }
    public virtual NotificationExecution? NextNotificationExecution { get; set; }
}