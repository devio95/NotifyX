using Application.Interfaces;
using Domain.Entities;

namespace Application.EntityServices.Notifications
{
    public  class GetNotificationsQueries(IUnitOfWork unitOfWork)
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Notification?> GetOneAsync(int notificationId)
        {
            Notification? toReturn = null;
            await _unitOfWork.BeginTransactionAsync();
            toReturn = await _unitOfWork.Notifications.GetByIdAsync(notificationId);
            await _unitOfWork.CommitTransactionAsync();
            return toReturn;
        }
    }
}