using Microsoft.EntityFrameworkCore;
using Eventourismo.Domain.Entities;
using Eventourismo.Domain.Interfaces;
using Eventourismo.Domain.ValueObjects;
using Eventourismo.Infrastructure.Data;

namespace Eventourismo.Infrastructure.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(EventourismoDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Event>> GetEventsByLocationAsync(Location location, double radiusKm)
    {
        // This is a simplified implementation. In a real application, you would use spatial queries
        // For now, we'll get all events and filter by a simple distance calculation
        var events = await _dbSet
            .Include(e => e.CreatedByUser)
            .Include(e => e.Venue)
            .Where(e => e.IsActive)
            .ToListAsync();

        return events.Where(e => e.Location.DistanceTo(location) <= radiusKm);
    }

    public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
    {
        return await _dbSet
            .Include(e => e.CreatedByUser)
            .Include(e => e.Venue)
            .Where(e => e.IsActive && e.StartTime > DateTime.UtcNow)
            .OrderBy(e => e.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByUserAsync(Guid userId)
    {
        return await _dbSet
            .Include(e => e.CreatedByUser)
            .Include(e => e.Venue)
            .Where(e => e.CreatedByUserId == userId)
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetEventsByVenueAsync(Guid venueId)
    {
        return await _dbSet
            .Include(e => e.CreatedByUser)
            .Include(e => e.Venue)
            .Where(e => e.VenueId == venueId && e.IsActive)
            .OrderBy(e => e.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> SearchEventsAsync(string searchTerm)
    {
        return await _dbSet
            .Include(e => e.CreatedByUser)
            .Include(e => e.Venue)
            .Where(e => e.IsActive && 
                       (e.Title.Contains(searchTerm) || 
                        e.Description.Contains(searchTerm) ||
                        e.Location.City.Contains(searchTerm)))
            .OrderBy(e => e.StartTime)
            .ToListAsync();
    }

    public async Task<IEnumerable<Event>> GetPopularEventsAsync(int count = 10)
    {
        return await _dbSet
            .Include(e => e.CreatedByUser)
            .Include(e => e.Venue)
            .Include(e => e.Likes)
            .Include(e => e.Favorites)
            .Where(e => e.IsActive && e.StartTime > DateTime.UtcNow)
            .OrderByDescending(e => e.Likes.Count + e.Favorites.Count)
            .Take(count)
            .ToListAsync();
    }

    public override async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(e => e.CreatedByUser)
            .Include(e => e.Venue)
            .Include(e => e.Likes)
            .Include(e => e.Favorites)
            .Include(e => e.Comments)
                .ThenInclude(c => c.User)
            .FirstOrDefaultAsync(e => e.Id == id);
    }
}