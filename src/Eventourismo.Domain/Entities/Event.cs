using Eventourismo.Domain.Enums;
using Eventourismo.Domain.ValueObjects;

namespace Eventourismo.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public EventType Type { get; private set; }
    public Location Location { get; private set; }
    public Guid CreatedByUserId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public string? ImageUrl { get; private set; }
    public int MaxAttendees { get; private set; }
    public decimal? TicketPrice { get; private set; }
    public bool IsPublic { get; private set; }
    public string? ContactInfo { get; private set; }
    public string? ExternalUrl { get; private set; }

    // Navigation properties
    public User CreatedByUser { get; private set; } = null!;
    public Venue? Venue { get; private set; }
    public Guid? VenueId { get; private set; }
    public ICollection<Favorite> Favorites { get; private set; } = new List<Favorite>();
    public ICollection<Like> Likes { get; private set; } = new List<Like>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

    public Event(string title, string description, DateTime startTime, EventType type, Location location, Guid createdByUserId, bool isPublic = true, int maxAttendees = 0)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty");
        
        if (startTime <= DateTime.UtcNow)
            throw new ArgumentException("Start time must be in the future");

        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        StartTime = startTime;
        Type = type;
        Location = location;
        CreatedByUserId = createdByUserId;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        IsPublic = isPublic;
        MaxAttendees = maxAttendees;
    }

    public void Update(string title, string description, DateTime startTime, DateTime? endTime = null, string? imageUrl = null, int maxAttendees = 0, decimal? ticketPrice = null, string? contactInfo = null, string? externalUrl = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty");
        
        if (startTime <= DateTime.UtcNow)
            throw new ArgumentException("Start time must be in the future");

        if (endTime.HasValue && endTime <= startTime)
            throw new ArgumentException("End time must be after start time");

        Title = title;
        Description = description;
        StartTime = startTime;
        EndTime = endTime;
        ImageUrl = imageUrl;
        MaxAttendees = maxAttendees;
        TicketPrice = ticketPrice;
        ContactInfo = contactInfo;
        ExternalUrl = externalUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AssignToVenue(Guid venueId)
    {
        VenueId = venueId;
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveFromVenue()
    {
        VenueId = null;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool HasEnded => EndTime.HasValue && EndTime < DateTime.UtcNow || StartTime.AddHours(4) < DateTime.UtcNow;
    public bool HasStarted => StartTime <= DateTime.UtcNow;
    public bool IsUpcoming => StartTime > DateTime.UtcNow;
    public int LikesCount => Likes.Count;
    public int CommentsCount => Comments.Count;
    public int FavoritesCount => Favorites.Count;
}