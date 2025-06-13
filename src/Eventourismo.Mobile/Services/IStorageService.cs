namespace Eventourismo.Mobile.Services;

public interface IStorageService
{
    Task<string?> GetAsync(string key);
    Task SetAsync(string key, string value);
    Task RemoveAsync(string key);
    Task ClearAsync();
}