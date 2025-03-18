namespace Domain.Entities;

public class NotifyExecution
{
    public required long Id { get; set; }
    public required int NotificationId { get; set; }
    public required bool Result { get; set; }
    public required DateTime ExecutionDate { get; set; }
    public required string FailDescriptionId { get; set; }
    public required string CustomFailDescription { get; set; }

    // Navigation properties

    public virtual Notification? Notification { get; set; }
}