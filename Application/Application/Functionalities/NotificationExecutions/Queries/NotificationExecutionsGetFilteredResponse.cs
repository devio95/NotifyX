using Application.DTO.NotificationExecutions;

namespace Application.Functionalities.NotificationExecutions.Queries
{
    public class NotificationExecutionsGetFilteredResponse
    {
        public IEnumerable<NotificationExecutionGetDto> Response { get; }

        public NotificationExecutionsGetFilteredResponse(IEnumerable<NotificationExecutionGetDto> dtos)
        {
            Response = dtos;
        }
    }
}