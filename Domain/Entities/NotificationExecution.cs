namespace Domain.Entities;

public class NotificationExecution
{
    public long Id { get; private set; }
    public int NotificationId { get; private set; }
    public bool? Result { get; private set; }
    public DateTime ExecutionDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public int? FailDescriptionId { get; private set; }
    public string CustomFailDescription { get; private set; } = string.Empty;
    public bool IsProcessing { get; private set; }

    protected NotificationExecution() { }

    // Navigation properties

    public virtual Notification? Notification { get; set; }

    public static NotificationExecution CreateForNotification(int notificationId, DateTime executionDate)
    {
        return new NotificationExecution()
        {
            NotificationId = notificationId,
            Result = null,
            ExecutionDate = executionDate,
            EndDate = null,
            FailDescriptionId = null,
            CustomFailDescription = string.Empty,
            IsProcessing = false
        };
    }

    public void SetIsProcessing()
    {
        IsProcessing = true;
    }

    public void FinishOk()
    {
        Result = true;
        EndDate = DateTime.UtcNow;
        FailDescriptionId = null;
        CustomFailDescription = string.Empty;
    }

    public void FinishNok(string error)
    {
        Result = false;
        EndDate = DateTime.UtcNow;
        FailDescriptionId = null;
        CustomFailDescription = error;
    }
}