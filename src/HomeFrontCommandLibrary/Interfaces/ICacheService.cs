namespace HomeFrontCommandLibrary.Interfaces;

internal interface ICacheService
{
    Task<object?> GetCachedData(string cacheKey);
    Task SetCachedData(string cacheKey, object data, TimeSpan time);
    Task DeleteCashedData(string cacheKey);
}