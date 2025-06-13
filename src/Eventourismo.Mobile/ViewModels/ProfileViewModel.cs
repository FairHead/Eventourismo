using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Eventourismo.Mobile.Models;
using Eventourismo.Mobile.Services;

namespace Eventourismo.Mobile.ViewModels;

public partial class ProfileViewModel : BaseViewModel
{
    private readonly IApiService _apiService;
    private readonly IStorageService _storageService;

    [ObservableProperty]
    private UserDto? currentUser;

    [ObservableProperty]
    private bool isLoggedIn;

    public ProfileViewModel(IApiService apiService, IStorageService storageService)
    {
        _apiService = apiService;
        _storageService = storageService;
        Title = "Profile";
    }

    public override async Task InitializeAsync()
    {
        await LoadUserProfileAsync();
    }

    [RelayCommand]
    private async Task LoadUserProfileAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;

            var token = await _storageService.GetAsync("auth_token");
            IsLoggedIn = !string.IsNullOrEmpty(token);

            if (IsLoggedIn)
            {
                var result = await _apiService.GetUserProfileAsync();
                if (result.Success && result.Data != null)
                {
                    CurrentUser = result.Data;
                }
                else
                {
                    IsLoggedIn = false;
                    await _storageService.RemoveAsync("auth_token");
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

    [RelayCommand]
    private async Task LogoutAsync()
    {
        try
        {
            await _storageService.RemoveAsync("auth_token");
            IsLoggedIn = false;
            CurrentUser = null;
            Console.WriteLine("Logged out - would navigate to login");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    [RelayCommand]
    private async Task GoToLoginAsync()
    {
        Console.WriteLine("Would navigate to login page");
        await Task.CompletedTask;
    }
}