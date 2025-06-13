using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Eventourismo.Mobile.Models;
using Eventourismo.Mobile.Services;

namespace Eventourismo.Mobile.ViewModels;

public partial class MapViewModel : BaseViewModel
{
    private readonly IApiService _apiService;
    private readonly ILocationService _locationService;

    [ObservableProperty]
    private ObservableCollection<EventDto> mapEvents = new();

    [ObservableProperty]
    private LocationDto? currentLocation;

    [ObservableProperty]
    private bool hasLocationPermission;

    public MapViewModel(IApiService apiService, ILocationService locationService)
    {
        _apiService = apiService;
        _locationService = locationService;
        Title = "Map";
    }

    public override async Task InitializeAsync()
    {
        await CheckLocationPermissionAsync();
        if (HasLocationPermission)
        {
            await LoadCurrentLocationAsync();
            await LoadMapEventsAsync();
        }
    }

    [RelayCommand]
    private async Task CheckLocationPermissionAsync()
    {
        HasLocationPermission = await _locationService.HasLocationPermissionAsync();
        if (!HasLocationPermission)
        {
            HasLocationPermission = await _locationService.RequestLocationPermissionAsync();
        }
    }

    [RelayCommand]
    private async Task LoadCurrentLocationAsync()
    {
        try
        {
            CurrentLocation = await _locationService.GetCurrentLocationAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task LoadMapEventsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var result = await _apiService.GetEventsAsync(
                CurrentLocation?.Latitude,
                CurrentLocation?.Longitude,
                25); // 25km radius for map view

            if (result.Success && result.Data != null)
            {
                MapEvents.Clear();
                foreach (var eventItem in result.Data)
                {
                    MapEvents.Add(eventItem);
                }
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
}