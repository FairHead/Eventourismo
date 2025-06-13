using Eventourismo.Mobile.Models;

namespace Eventourismo.Mobile.Services;

public interface IApiService
{
    Task<ApiResponse<List<EventDto>>> GetEventsAsync(double? latitude = null, double? longitude = null, double radiusKm = 10);
    Task<ApiResponse<EventDto>> CreateEventAsync(EventDto eventDto);
    Task<ApiResponse<string>> LoginAsync(string email, string password);
    Task<ApiResponse<UserDto>> RegisterAsync(string email, string userName, string firstName, string lastName, string password);
    Task<ApiResponse<UserDto>> GetUserProfileAsync();
}