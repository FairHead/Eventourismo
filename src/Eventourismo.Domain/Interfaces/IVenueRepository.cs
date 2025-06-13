using Eventourismo.Domain.Entities;
using Eventourismo.Domain.ValueObjects;

namespace Eventourismo.Domain.Interfaces;

public interface IVenueRepository : IRepository<Venue>
{
    Task<IEnumerable<Venue>> GetVenuesByLocationAsync(Location location, double radiusKm);
    Task<IEnumerable<Venue>> SearchVenuesAsync(string searchTerm);
    Task<IEnumerable<Venue>> GetActiveVenuesAsync();
}