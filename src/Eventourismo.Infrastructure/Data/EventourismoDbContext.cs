using Microsoft.EntityFrameworkCore;
using Eventourismo.Domain.Entities;
using Eventourismo.Domain.ValueObjects;

namespace Eventourismo.Infrastructure.Data;

public class EventourismoDbContext : DbContext
{
    public EventourismoDbContext(DbContextOptions<EventourismoDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User entity configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.UserName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).HasConversion<int>();
            entity.Property(e => e.ProfileImageUrl).HasMaxLength(500);
            entity.Property(e => e.Bio).HasMaxLength(1000);
            
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasIndex(e => e.UserName).IsUnique();
        });

        // Event entity configuration
        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.Type).HasConversion<int>();
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
            entity.Property(e => e.ContactInfo).HasMaxLength(500);
            entity.Property(e => e.ExternalUrl).HasMaxLength(500);
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(18,2)");

            // Configure Location as owned entity
            entity.OwnsOne(e => e.Location, location =>
            {
                location.Property(l => l.Latitude).IsRequired();
                location.Property(l => l.Longitude).IsRequired();
                location.Property(l => l.Address).IsRequired().HasMaxLength(300);
                location.Property(l => l.City).IsRequired().HasMaxLength(100);
                location.Property(l => l.Country).HasMaxLength(100);
            });

            // Relationships
            entity.HasOne(e => e.CreatedByUser)
                  .WithMany(u => u.CreatedEvents)
                  .HasForeignKey(e => e.CreatedByUserId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Venue)
                  .WithMany(v => v.Events)
                  .HasForeignKey(e => e.VenueId)
                  .OnDelete(DeleteBehavior.SetNull);
        });

        // Venue entity configuration
        modelBuilder.Entity<Venue>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
            entity.Property(e => e.ContactEmail).HasMaxLength(256);
            entity.Property(e => e.ContactPhone).HasMaxLength(50);
            entity.Property(e => e.Website).HasMaxLength(500);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);

            // Configure Location as owned entity
            entity.OwnsOne(e => e.Location, location =>
            {
                location.Property(l => l.Latitude).IsRequired();
                location.Property(l => l.Longitude).IsRequired();
                location.Property(l => l.Address).IsRequired().HasMaxLength(300);
                location.Property(l => l.City).IsRequired().HasMaxLength(100);
                location.Property(l => l.Country).HasMaxLength(100);
            });

            // Configure OpeningHours as owned entities
            entity.OwnsMany(e => e.OpeningHours, oh =>
            {
                oh.Property(o => o.DayOfWeek).HasConversion<int>();
                oh.Property(o => o.OpenTime);
                oh.Property(o => o.CloseTime);
                oh.Property(o => o.IsClosed);
            });
        });

        // Favorite entity configuration
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(f => f.User)
                  .WithMany(u => u.Favorites)
                  .HasForeignKey(f => f.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(f => f.Event)
                  .WithMany(e => e.Favorites)
                  .HasForeignKey(f => f.EventId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(f => new { f.UserId, f.EventId }).IsUnique();
        });

        // Like entity configuration
        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.HasOne(l => l.User)
                  .WithMany(u => u.Likes)
                  .HasForeignKey(l => l.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(l => l.Event)
                  .WithMany(e => e.Likes)
                  .HasForeignKey(l => l.EventId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(l => new { l.UserId, l.EventId }).IsUnique();
        });

        // Comment entity configuration
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Content).IsRequired().HasMaxLength(1000);
            
            entity.HasOne(c => c.User)
                  .WithMany(u => u.Comments)
                  .HasForeignKey(c => c.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(c => c.Event)
                  .WithMany(e => e.Comments)
                  .HasForeignKey(c => c.EventId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}