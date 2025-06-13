using Eventourismo.Domain.ValueObjects;

namespace Eventourismo.Domain.Entities;

public class Venue
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Location Location { get; private set; }
    public string? ContactEmail { get; private set; }
    public string? ContactPhone { get; private set; }
    public string? Website { get; private set; }
    public int Capacity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }
    public string? ImageUrl { get; private set; }
    public ICollection<OpeningHours>? OpeningHours { get; private set; }

    // Navigation properties
    public ICollection<Event> Events { get; private set; } = new List<Event>();

    public Venue(string name, string description, Location location, int capacity = 0)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty");
        
        if (capacity < 0)
            throw new ArgumentException("Capacity cannot be negative");

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Location = location;
        Capacity = capacity;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void Update(string name, string description, int capacity, string? contactEmail = null, string? contactPhone = null, string? website = null, string? imageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be empty");
        
        if (capacity < 0)
            throw new ArgumentException("Capacity cannot be negative");

        Name = name;
        Description = description;
        Capacity = capacity;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
        Website = website;
        ImageUrl = imageUrl;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetOpeningHours(ICollection<OpeningHours> openingHours)
    {
        OpeningHours = openingHours;
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

    public bool IsOpenAt(DateTime dateTime)
    {
        if (OpeningHours == null || !OpeningHours.Any()) return true; // Assume open if no hours set
        
        var dayOpeningHours = OpeningHours.FirstOrDefault(oh => oh.DayOfWeek == dateTime.DayOfWeek);
        if (dayOpeningHours == null) return false;
        
        return dayOpeningHours.IsOpenAt(TimeOnly.FromDateTime(dateTime));
    }
}