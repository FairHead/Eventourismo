namespace Eventourismo.Mobile.Services;

public class StorageService : IStorageService
{
    private readonly Dictionary<string, string> _storage = new();

    public async Task<string?> GetAsync(string key)
    {
        try
        {
            await Task.Delay(10); // Simulate async operation
            return _storage.TryGetValue(key, out var value) ? value : null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task SetAsync(string key, string value)
    {
        try
        {
            await Task.Delay(10); // Simulate async operation
            _storage[key] = value;
        }
        catch (Exception)
        {
            // Handle storage errors
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await Task.Delay(10); // Simulate async operation
            _storage.Remove(key);
        }
        catch (Exception)
        {
            // Handle storage errors
        }
    }

    public async Task ClearAsync()
    {
        try
        {
            await Task.Delay(10); // Simulate async operation
            _storage.Clear();
        }
        catch (Exception)
        {
            // Handle storage errors
        }
    }
}