using Application.DTO.NotificationExecutions;
using Application.Functionalities.NotificationExecutions.Queries;
using Application.Interfaces;
using Application.Interfaces.Exceptions;
using Domain.Entities;
using MediatR;

namespace Application.EntityServices.NotificationExecutions.Queries;

public class NotificationExecutionsGetFilteredQuery : IRequest<NotificationExecutionsGetFilteredResponse>
{
    public int MinutesPast { get; }

    public NotificationExecutionsGetFilteredQuery(int minutesPast)
    {
        if (minutesPast < 0 )
        {
            throw new DataValidationException("MinutesPast must be greater than 0");
        }

        MinutesPast = minutesPast;
    }
}

public class NotificationExecutionsGetFilteredQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<NotificationExecutionsGetFilteredQuery, NotificationExecutionsGetFilteredResponse>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<NotificationExecutionsGetFilteredResponse> Handle(NotificationExecutionsGetFilteredQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<NotificationExecution> toReturn = [];

        toReturn = await _unitOfWork.NotificationExecutions.GetAsync(DateTime.UtcNow, request.MinutesPast);

        return new NotificationExecutionsGetFilteredResponse(toReturn.Select(x => new NotificationExecutionsGetFilteredDto(x)));
    }
}