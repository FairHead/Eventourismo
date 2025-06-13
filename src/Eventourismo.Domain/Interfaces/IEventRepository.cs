using Eventourismo.Domain.Entities;
using Eventourismo.Domain.ValueObjects;

namespace Eventourismo.Domain.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<IEnumerable<Event>> GetEventsByLocationAsync(Location location, double radiusKm);
    Task<IEnumerable<Event>> GetUpcomingEventsAsync();
    Task<IEnumerable<Event>> GetEventsByUserAsync(Guid userId);
    Task<IEnumerable<Event>> GetEventsByVenueAsync(Guid venueId);
    Task<IEnumerable<Event>> SearchEventsAsync(string searchTerm);
    Task<IEnumerable<Event>> GetPopularEventsAsync(int count = 10);
}