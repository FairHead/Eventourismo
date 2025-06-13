using System.Text.Json;
using System.Text;
using Eventourismo.Mobile.Models;

namespace Eventourismo.Mobile.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://localhost:7000/api"; // Update with your API URL

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
    }

    public async Task<ApiResponse<List<EventDto>>> GetEventsAsync(double? latitude = null, double? longitude = null, double radiusKm = 10)
    {
        try
        {
            var queryString = $"?radiusKm={radiusKm}";
            if (latitude.HasValue && longitude.HasValue)
            {
                queryString += $"&latitude={latitude}&longitude={longitude}";
            }

            var response = await _httpClient.GetAsync($"/events{queryString}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<List<EventDto>>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse ?? new ApiResponse<List<EventDto>> { Success = false, Message = "Failed to parse response" };
            }

            return new ApiResponse<List<EventDto>> { Success = false, Message = $"API call failed: {response.StatusCode}" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<EventDto>> { Success = false, Message = ex.Message };
        }
    }

    public async Task<ApiResponse<EventDto>> CreateEventAsync(EventDto eventDto)
    {
        try
        {
            var json = JsonSerializer.Serialize(eventDto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/events", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<EventDto>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse ?? new ApiResponse<EventDto> { Success = false, Message = "Failed to parse response" };
            }

            return new ApiResponse<EventDto> { Success = false, Message = $"API call failed: {response.StatusCode}" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<EventDto> { Success = false, Message = ex.Message };
        }
    }

    public async Task<ApiResponse<string>> LoginAsync(string email, string password)
    {
        try
        {
            var loginRequest = new { Email = email, Password = password };
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/auth/login", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<string>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse ?? new ApiResponse<string> { Success = false, Message = "Failed to parse response" };
            }

            return new ApiResponse<string> { Success = false, Message = $"Login failed: {response.StatusCode}" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<string> { Success = false, Message = ex.Message };
        }
    }

    public async Task<ApiResponse<UserDto>> RegisterAsync(string email, string userName, string firstName, string lastName, string password)
    {
        try
        {
            var registerRequest = new { Email = email, UserName = userName, FirstName = firstName, LastName = lastName, Password = password };
            var json = JsonSerializer.Serialize(registerRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/auth/register", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<UserDto>>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse ?? new ApiResponse<UserDto> { Success = false, Message = "Failed to parse response" };
            }

            return new ApiResponse<UserDto> { Success = false, Message = $"Registration failed: {response.StatusCode}" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserDto> { Success = false, Message = ex.Message };
        }
    }

    public async Task<ApiResponse<UserDto>> GetUserProfileAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("/users/profile");
            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<ApiResponse<UserDto>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse ?? new ApiResponse<UserDto> { Success = false, Message = "Failed to parse response" };
            }

            return new ApiResponse<UserDto> { Success = false, Message = $"API call failed: {response.StatusCode}" };
        }
        catch (Exception ex)
        {
            return new ApiResponse<UserDto> { Success = false, Message = ex.Message };
        }
    }
}