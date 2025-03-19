using Domain.Enums;

namespace Domain.Entities;

public class NotificationMethod
{
    public required NotificationMethodEnum Id { get; set; }
    public required string Name { get; set; }
}