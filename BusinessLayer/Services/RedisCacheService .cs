using BusinessLayer.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        var jsonData = JsonSerializer.Serialize(value);
        _cache.SetString(key, jsonData, options);
    }

    public bool TryGet<T>(string key, out T value)
    {
        var jsonData = _cache.GetString(key);
        if (jsonData == null)
        {
            value = default!;
            return false;
        }

        value = JsonSerializer.Deserialize<T>(jsonData)!;
        return true;
    }

    public void Remove(string key)
    {
        _cache.Remove(key);
    }
}
