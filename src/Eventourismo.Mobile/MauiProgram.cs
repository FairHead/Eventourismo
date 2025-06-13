using Microsoft.Extensions.Logging;
using Eventourismo.Mobile.Services;
using Eventourismo.Mobile.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Eventourismo.Mobile;

public static class MobileAppModule
{
    public static IServiceCollection AddMobileServices(this IServiceCollection services)
    {
        // Register Services
        services.AddHttpClient<IApiService, ApiService>();
        services.AddSingleton<ILocationService, LocationService>();
        services.AddSingleton<IStorageService, StorageService>();

        // Register ViewModels
        services.AddTransient<LoginViewModel>();
        services.AddTransient<MapViewModel>();
        services.AddTransient<EventsViewModel>();
        services.AddTransient<ProfileViewModel>();

        return services;
    }
}