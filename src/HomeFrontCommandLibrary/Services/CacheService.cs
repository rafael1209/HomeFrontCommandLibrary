using HomeFrontCommandLibrary.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace HomeFrontCommandLibrary.Services;

internal class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

    public Task<object?> GetCachedData(string cacheKey)
    {
        _memoryCache.TryGetValue(cacheKey, out var objectData);

        return Task.FromResult(objectData);
    }

    public Task SetCachedData(string cacheKey, object data, TimeSpan time)
    {
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(time);

        _memoryCache.Set(cacheKey, data, cacheEntryOptions);

        return Task.CompletedTask;
    }

    public Task DeleteCashedData(string cacheKey)
    {
        _memoryCache.Remove(cacheKey);

        return Task.CompletedTask;
    }
}