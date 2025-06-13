namespace Eventourismo.Domain.Entities;

public class Like
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    public Like(Guid userId, Guid eventId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        EventId = eventId;
        CreatedAt = DateTime.UtcNow;
    }
}