using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Eventourismo.Mobile.Services;

namespace Eventourismo.Mobile.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IApiService _apiService;
    private readonly IStorageService _storageService;

    [ObservableProperty]
    private string email = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private bool isLoginMode = true;

    [ObservableProperty]
    private string firstName = string.Empty;

    [ObservableProperty]
    private string lastName = string.Empty;

    [ObservableProperty]
    private string userName = string.Empty;

    public LoginViewModel(IApiService apiService, IStorageService storageService)
    {
        _apiService = apiService;
        _storageService = storageService;
        Title = "Login";
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (IsBusy) return;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
        {
            Console.WriteLine("Error: Please enter both email and password");
            return;
        }

        try
        {
            IsBusy = true;

            var result = await _apiService.LoginAsync(Email, Password);
            if (result.Success && result.Data != null)
            {
                await _storageService.SetAsync("auth_token", result.Data);
                Console.WriteLine("Login successful - would navigate to Map");
            }
            else
            {
                Console.WriteLine($"Login failed: {result.Message}");
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
    private async Task RegisterAsync()
    {
        if (IsBusy) return;

        if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(FirstName) ||
            string.IsNullOrWhiteSpace(LastName))
        {
            Console.WriteLine("Error: Please fill all fields");
            return;
        }

        try
        {
            IsBusy = true;

            var result = await _apiService.RegisterAsync(Email, UserName, FirstName, LastName, Password);
            if (result.Success)
            {
                Console.WriteLine("Registration successful! Please login.");
                ToggleMode();
            }
            else
            {
                Console.WriteLine($"Registration failed: {result.Message}");
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
    private void ToggleMode()
    {
        IsLoginMode = !IsLoginMode;
        Title = IsLoginMode ? "Login" : "Register";
    }
}