using Application.DTO.NotificationExecutions;

namespace Application.Functionalities.NotificationExecutions.Queries
{
    public class NotificationExecutionsGetFilteredResponse
    {
        public IEnumerable<NotificationExecutionsGetFilteredDto> Response { get; }

        public NotificationExecutionsGetFilteredResponse(IEnumerable<NotificationExecutionsGetFilteredDto> dtos)
        {
            Response = dtos;
        }
    }
}