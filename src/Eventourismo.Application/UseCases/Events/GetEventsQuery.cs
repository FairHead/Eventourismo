using MediatR;
using Eventourismo.Application.DTOs;

namespace Eventourismo.Application.UseCases.Events;

public class GetEventsQuery : IRequest<List<EventDto>>
{
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double RadiusKm { get; set; } = 10;
    public bool UpcomingOnly { get; set; } = true;
}

public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<EventDto>>
{
    // Implementation will be added in Infrastructure layer
    public Task<List<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}