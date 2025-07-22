using Application.DTO.NotificationExecutions;
using Application.Exceptions;
using Application.Functionalities.NotificationExecutions.Queries;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

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

        return new NotificationExecutionsGetFilteredResponse(toReturn.Select(x => new NotificationExecutionGetDto(x)));
    }
}