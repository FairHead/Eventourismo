using Eventourismo.Domain.Enums;

namespace Eventourismo.Application.DTOs;

public class EventDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public EventType Type { get; set; }
    public LocationDto Location { get; set; } = new();
    public Guid CreatedByUserId { get; set; }
    public string CreatedByUserName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public string? ImageUrl { get; set; }
    public int MaxAttendees { get; set; }
    public decimal? TicketPrice { get; set; }
    public bool IsPublic { get; set; }
    public string? ContactInfo { get; set; }
    public string? ExternalUrl { get; set; }
    public VenueDto? Venue { get; set; }
    public int LikesCount { get; set; }
    public int CommentsCount { get; set; }
    public int FavoritesCount { get; set; }
    public bool HasEnded { get; set; }
    public bool HasStarted { get; set; }
    public bool IsUpcoming { get; set; }
}