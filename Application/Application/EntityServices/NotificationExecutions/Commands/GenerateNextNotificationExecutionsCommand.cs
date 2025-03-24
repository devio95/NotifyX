using Application.Interfaces;
using Domain.Entities;

namespace Application.EntityServices.NotificationExecutions.Commands
{
    public class GenerateNextNotificationExecutionsCommand(IUnitOfWork unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task GenerateNextNotificationExecutionAsync(int notificationId)
        {
            await _unitOfWork.BeginTransactionAsync();
            Notification? notification = await _unitOfWork.Notifications.GetByIdAsync(notificationId);
            if (notification == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }

            if (notification.NotificationTypeId == NotificationTypeEnum.Single)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }

            if (notification.NextNotificationExecution == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }

            if (notification.NextNotificationExecution.Result == null)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return;
            }

            DateTime nextExecutionDate = GetNotificationExecution(notification);
            NotificationExecution nextNotificationExecution = await SaveNotificationExecutionAsync(notification, nextExecutionDate);
            await _unitOfWork.SaveChangesAsync();
            
            notification.NextNotificationExecutionId = nextNotificationExecution.Id;
            notification.NextNotificationExecution = nextNotificationExecution;

            _unitOfWork.Notifications.Update(notification);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }

        private async Task<NotificationExecution> SaveNotificationExecutionAsync(Notification notification, DateTime notificationExecutionDate)
        {
            return await _unitOfWork.NotificationExecutions.AddAsync(new NotificationExecution()
            {
                Id = 0,
                NotificationId = notification.Id,
                Result = null,
                ExecutionDate = notificationExecutionDate,
                EndDate = null,
                FailDescriptionId = null,
                CustomFailDescription = string.Empty,
                IsProcessing = false
            });
        }

        private DateTime GetNotificationExecution(Notification notification)
        {
            switch (notification.NotificationTypeId)
            {
                case NotificationTypeEnum.Minute:
                    return CalculateExecutionDateMinute(notification);
                case NotificationTypeEnum.Day:
                    return CalculateExecutionDateDay(notification);
                case NotificationTypeEnum.Week:
                    return CalculateExecutionDateWeek(notification);
                case NotificationTypeEnum.Month:
                    return CalculateExecutionDateMonth(notification);
                case NotificationTypeEnum.Year:
                    return CalculateExecutionDateYear(notification);
                default:
                    throw new Exception();
            }
        }

        private DateTime CalculateExecutionDateMinute(Notification notification)
        {
            DateTime now = DateTime.UtcNow;
            DateTime toReturn = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, DateTimeKind.Utc);
            return toReturn.AddMinutes(1);
        }

        private DateTime CalculateExecutionDateDay(Notification notification)
        {
            DateTime now = DateTime.UtcNow;
            DateTime executionToCompare = new DateTime(now.Year, now.Month, now.Day, notification.ExecutionStart.Hour, notification.ExecutionStart.Minute, 0, DateTimeKind.Utc);
            if (now > executionToCompare)
            {
                return executionToCompare.AddDays(1);
            }
            else
            {
                return executionToCompare;
            }
        }

        private DateTime CalculateExecutionDateWeek(Notification notification)
        {
            DateTime now = DateTime.UtcNow;
            DayOfWeek targetDayOfWeek = notification.ExecutionStart.DayOfWeek;
            int daysToAdd = ((int)targetDayOfWeek - (int)now.DayOfWeek + 7) % 7;
            if (daysToAdd == 0)
            {
                DateTime todayTargetTime = new DateTime(now.Year, now.Month, now.Day,
                    notification.ExecutionStart.Hour, notification.ExecutionStart.Minute, 0, DateTimeKind.Utc);

                if (now > todayTargetTime)
                {
                    daysToAdd = 7;
                }
            }

            return new DateTime(now.Year, now.Month, now.Day,
                notification.ExecutionStart.Hour, notification.ExecutionStart.Minute, 0, DateTimeKind.Utc)
                .AddDays(daysToAdd);
        }

        private DateTime CalculateExecutionDateMonth(Notification notification)
        {
            DateTime now = DateTime.UtcNow;

            int targetDay = notification.ExecutionStart.Day;
            int daysInCurrentMonth = DateTime.DaysInMonth(now.Year, now.Month);

            if (targetDay > daysInCurrentMonth)
            {
                targetDay = daysInCurrentMonth;
            }

            DateTime executionToCompare = new DateTime(
                now.Year,
                now.Month,
                targetDay,
                notification.ExecutionStart.Hour,
                notification.ExecutionStart.Minute,
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
                    notification.ExecutionStart.Hour,
                    notification.ExecutionStart.Minute,
                    0,
                    DateTimeKind.Utc);
            }
            else
            {
                return executionToCompare;
            }
        }

        private DateTime CalculateExecutionDateYear(Notification notification)
        {
            DateTime now = DateTime.UtcNow;

            int targetMonth = notification.ExecutionStart.Month;
            int targetDay = notification.ExecutionStart.Day;

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
                notification.ExecutionStart.Hour,
                notification.ExecutionStart.Minute,
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
                    notification.ExecutionStart.Hour,
                    notification.ExecutionStart.Minute,
                    0,
                    DateTimeKind.Utc);
            }
            else
            {
                return executionToCompare;
            }
        }
    }
}