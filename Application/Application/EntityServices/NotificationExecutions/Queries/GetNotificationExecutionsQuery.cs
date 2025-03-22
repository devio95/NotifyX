using Application.Interfaces;
using Domain.Entities;

namespace Application.EntityServices.NotificationExecutions.Queries
{
    public class GetNotificationExecutionsQuery(IUnitOfWork unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<NotificationExecution>> Get()
        {
            IEnumerable<NotificationExecution> toReturn = [];

            await _unitOfWork.BeginTransactionAsync();
            toReturn = await _unitOfWork.NotificationExecutions.GetAsync(DateTime.UtcNow, 5);
            await _unitOfWork.CommitTransactionAsync();

            return toReturn;
        }
    }
}