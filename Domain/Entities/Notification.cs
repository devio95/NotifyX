using Domain.Entities.Users;
using Domain.Enums;

namespace Domain.Entities;

public class Notification
{
    public int Id { get; private set; }
    public int UserId { get; set; }
    public long? NextNotificationExecutionId { get; private set; }
    public string Subject { get; private set; } = string.Empty;
    public string Text { get; private set; } = string.Empty;
    public NotificationMethodEnum NotificationMethodId { get; private set; }
    public NotificationTypeEnum NotificationTypeId { get; private set; }
    public DateTime? EndDate { get; private set; }
    public DateTime ExecutionStart { get; private set; }

    public Notification() { }

    // Nagivation properties

    public virtual NotificationMethod? NotificationMethod { get; set; }
    public virtual NotificationType? NotificationType { get; set; }
    public virtual User? User { get; set; }
    public virtual NotificationExecution? NextNotificationExecution { get; set; }

    public void SetNextNotificationExecution(NotificationExecution? notificationExecution)
    {
        if (notificationExecution == null)
        {
            ClearNextNotificationExecution();
        }
        else
        {
            NextNotificationExecutionId = notificationExecution.Id;
            NextNotificationExecution = notificationExecution;
        }
    }

    private void ClearNextNotificationExecution()
    {
        NextNotificationExecutionId = null;
        NextNotificationExecution = null;
    }

    public NotificationExecution? ScheduleNextNotificationExecution()
    {
        if (NotificationTypeId == NotificationTypeEnum.Single)
        {
            return null;
        }

        DateTime nextNotificationExecutionDate = CalculateNextNotificationExecution();
        return NotificationExecution.CreateForNotification(Id, nextNotificationExecutionDate);
    }

    private DateTime CalculateNextNotificationExecution()
    {
        switch (NotificationTypeId)
        {
            case NotificationTypeEnum.Minute:
                return CalculateExecutionDateMinute();
            case NotificationTypeEnum.Day:
                return CalculateExecutionDateDay();
            case NotificationTypeEnum.Week:
                return CalculateExecutionDateWeek();
            case NotificationTypeEnum.Month:
                return CalculateExecutionDateMonth();
            case NotificationTypeEnum.Year:
                return CalculateExecutionDateYear();
            default:
                throw new Exception();
        }
    }

    private DateTime CalculateExecutionDateMinute()
    {
        DateTime now = DateTime.UtcNow;
        DateTime toReturn = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Utc);
        return toReturn.AddMinutes(1);
    }

    private DateTime CalculateExecutionDateDay()
    {
        DateTime now = DateTime.UtcNow;
        DateTime executionToCompare = new DateTime(now.Year, now.Month, now.Day, ExecutionStart.Hour, ExecutionStart.Minute, 0, DateTimeKind.Utc);
        if (now > executionToCompare)
        {
            return executionToCompare.AddDays(1);
        }
        else
        {
            return executionToCompare;
        }
    }

    private DateTime CalculateExecutionDateWeek()
    {
        DateTime now = DateTime.UtcNow;
        DayOfWeek targetDayOfWeek = ExecutionStart.DayOfWeek;
        int daysToAdd = ((int)targetDayOfWeek - (int)now.DayOfWeek + 7) % 7;
        if (daysToAdd == 0)
        {
            DateTime todayTargetTime = new DateTime(now.Year, now.Month, now.Day, ExecutionStart.Hour, ExecutionStart.Minute, 0, DateTimeKind.Utc);

            if (now > todayTargetTime)
            {
                daysToAdd = 7;
            }
        }

        return new DateTime(now.Year, now.Month, now.Day, ExecutionStart.Hour, ExecutionStart.Minute, 0, DateTimeKind.Utc)
            .AddDays(daysToAdd);
    }

    private DateTime CalculateExecutionDateMonth()
    {
        DateTime now = DateTime.UtcNow;

        int targetDay = ExecutionStart.Day;
        int daysInCurrentMonth = DateTime.DaysInMonth(now.Year, now.Month);

        if (targetDay > daysInCurrentMonth)
        {
            targetDay = daysInCurrentMonth;
        }

        DateTime executionToCompare = new DateTime(
            now.Year,
            now.Month,
            targetDay,
            ExecutionStart.Hour,
            ExecutionStart.Minute,
            0,
            DateTimeKind.Utc);

        if (now > executionToCompare)
        {
            DateTime nextMonth = now.AddMonths(1);

            int daysInNextMonth = DateTime.DaysInMonth(nextMonth.Year, nextMonth.Month);
            int nextMonthTargetDay = targetDay;

            if (nextMonthTargetDay > daysInNextMonth)
            {
                nextMonthTargetDay = daysInNextMonth;
            }

            return new DateTime(
                nextMonth.Year,
                nextMonth.Month,
                nextMonthTargetDay,
                ExecutionStart.Hour,
                ExecutionStart.Minute,
                0,
                DateTimeKind.Utc);
        }
        else
        {
            return executionToCompare;
        }
    }

    private DateTime CalculateExecutionDateYear()
    {
        DateTime now = DateTime.UtcNow;

        int targetMonth = ExecutionStart.Month;
        int targetDay = ExecutionStart.Day;

        int targetYear = now.Year;

        int daysInTargetMonth = DateTime.DaysInMonth(targetYear, targetMonth);
        if (targetDay > daysInTargetMonth)
        {
            targetDay = daysInTargetMonth;
        }

        DateTime executionToCompare = new DateTime(
            targetYear,
            targetMonth,
            targetDay,
            ExecutionStart.Hour,
            ExecutionStart.Minute,
            0,
            DateTimeKind.Utc);

        if (now > executionToCompare)
        {
            targetYear = now.Year + 1;

            daysInTargetMonth = DateTime.DaysInMonth(targetYear, targetMonth);
            if (targetDay > daysInTargetMonth)
            {
                targetDay = daysInTargetMonth;
            }

            return new DateTime(
                targetYear,
                targetMonth,
                targetDay,
                ExecutionStart.Hour,
                ExecutionStart.Minute,
                0,
                DateTimeKind.Utc);
        }
        else
        {
            return executionToCompare;
        }
    }
}