using Domain.Entities;

namespace Application.DTO.NotificationExecutions;

public class NotificationExecutionGetDto
{
    public long Id { get; }
    public bool? Result { get; }
    public DateTime ExecutionDate { get; }
    public DateTime? EndDate { get; }
    public string CustomFailDescription { get; } = string.Empty;
    public bool IsProcessing { get; }

    public NotificationExecutionGetDto(NotificationExecution ne)
    {
        Id = ne.Id;
        Result = ne.Result;
        ExecutionDate = ne.ExecutionDate;
        EndDate = ne.EndDate;
        CustomFailDescription = ne.CustomFailDescription;
        IsProcessing = ne.IsProcessing;
    }
}