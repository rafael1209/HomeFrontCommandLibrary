namespace HomeFrontCommandLibrary.Interfaces;

public interface ICacheService
{
    Task<object?> GetCachedData(string cacheKey);
    Task SetCachedData(string cacheKey, object data, TimeSpan time);
    Task DeleteCashedData(string cacheKey);
}