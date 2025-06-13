using Eventourismo.Mobile.Models;

namespace Eventourismo.Mobile.Services;

public interface ILocationService
{
    Task<LocationDto?> GetCurrentLocationAsync();
    Task<bool> HasLocationPermissionAsync();
    Task<bool> RequestLocationPermissionAsync();
}