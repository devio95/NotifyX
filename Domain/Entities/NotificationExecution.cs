﻿namespace Domain.Entities;

public class NotificationExecution
{
    public required long Id { get; set; }
    public required int NotificationId { get; set; }
    public required bool? Result { get; set; }
    public required DateTime ExecutionDate { get; set; }
    public required DateTime? EndDate { get; set; }
    public required int? FailDescriptionId { get; set; }
    public required string CustomFailDescription { get; set; }
    public required bool IsProcessing { get; set; }

    // Navigation properties

    public virtual Notification? Notification { get; set; }

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