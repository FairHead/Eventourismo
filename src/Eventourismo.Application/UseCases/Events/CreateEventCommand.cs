using MediatR;
using Eventourismo.Application.DTOs;
using Eventourismo.Domain.Enums;

namespace Eventourismo.Application.UseCases.Events;

public class CreateEventCommand : IRequest<EventDto>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public EventType Type { get; set; }
    public LocationDto Location { get; set; } = new();
    public Guid CreatedByUserId { get; set; }
    public bool IsPublic { get; set; } = true;
    public int MaxAttendees { get; set; }
    public decimal? TicketPrice { get; set; }
    public string? ContactInfo { get; set; }
    public string? ExternalUrl { get; set; }
    public string? ImageUrl { get; set; }
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
{
    // Implementation will be added in Infrastructure layer
    public Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}