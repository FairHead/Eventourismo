using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Eventourismo.Mobile.Models;
using Eventourismo.Mobile.Services;

namespace Eventourismo.Mobile.ViewModels;

public partial class EventsViewModel : BaseViewModel
{
    private readonly IApiService _apiService;
    private readonly ILocationService _locationService;

    [ObservableProperty]
    private ObservableCollection<EventDto> events = new();

    [ObservableProperty]
    private LocationDto? currentLocation;

    public EventsViewModel(IApiService apiService, ILocationService locationService)
    {
        _apiService = apiService;
        _locationService = locationService;
        Title = "Events";
    }

    public override async Task InitializeAsync()
    {
        await LoadEventsAsync();
    }

    [RelayCommand]
    private async Task LoadEventsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            // Get current location if available
            CurrentLocation = await _locationService.GetCurrentLocationAsync();

            var result = await _apiService.GetEventsAsync(
                CurrentLocation?.Latitude,
                CurrentLocation?.Longitude,
                10); // 10km radius

            if (result.Success && result.Data != null)
            {
                Events.Clear();
                foreach (var eventItem in result.Data)
                {
                    Events.Add(eventItem);
                }
            }
            else
            {
                Console.WriteLine($"Failed to load events: {result.Message}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task CreateEventAsync()
    {
        // Navigate to create event page
        Console.WriteLine("Would navigate to create event page");
        await Task.CompletedTask;
    }
}