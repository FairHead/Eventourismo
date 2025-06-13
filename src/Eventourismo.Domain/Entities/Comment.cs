namespace Eventourismo.Domain.Entities;

public class Comment
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public Guid UserId { get; private set; }
    public Guid EventId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }

    // Navigation properties
    public User User { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    public Comment(string content, Guid userId, Guid eventId)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty");

        Id = Guid.NewGuid();
        Content = content;
        UserId = userId;
        EventId = eventId;
        CreatedAt = DateTime.UtcNow;
        IsDeleted = false;
    }

    public void Update(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("Content cannot be empty");

        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}