using Eventourismo.Domain.Enums;

namespace Eventourismo.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string PasswordHash { get; private set; }
    public UserRole Role { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public bool IsActive { get; private set; }
    public string? ProfileImageUrl { get; private set; }
    public string? Bio { get; private set; }

    // Navigation properties
    public ICollection<Event> CreatedEvents { get; private set; } = new List<Event>();
    public ICollection<Favorite> Favorites { get; private set; } = new List<Favorite>();
    public ICollection<Like> Likes { get; private set; } = new List<Like>();
    public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

    public User(string email, string userName, string firstName, string lastName, string passwordHash, UserRole role = UserRole.User)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");
        
        if (string.IsNullOrWhiteSpace(userName))
            throw new ArgumentException("UserName cannot be empty");
        
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("FirstName cannot be empty");
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("LastName cannot be empty");
        
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("PasswordHash cannot be empty");

        Id = Guid.NewGuid();
        Email = email;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
        PasswordHash = passwordHash;
        Role = role;
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
    }

    public void UpdateProfile(string firstName, string lastName, string? bio = null, string? profileImageUrl = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("FirstName cannot be empty");
        
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("LastName cannot be empty");

        FirstName = firstName;
        LastName = lastName;
        Bio = bio;
        ProfileImageUrl = profileImageUrl;
    }

    public void UpdateLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public string FullName => $"{FirstName} {LastName}";
}