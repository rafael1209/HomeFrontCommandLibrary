using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Services;

public class CityService(IMemoryCache memoryCache, Language language = Language.Hebrew) : ICityService
{
    private readonly HttpClient _httpClient = new();

    public async Task<City> GetCityByName(string cityName)
    {
        var cites = await GetCities();

        var city = cites.FirstOrDefault(c => c.LabelHebrew.Equals(cityName, StringComparison.OrdinalIgnoreCase)) ??
                   throw new Exception($"City '{cityName}' not found.");

        var cityData = new City
        {
            AreaId = city.AreaId,
            Name = city.Label,
            AreaName = city.AreaName,
            ProtectionTime = city.MigunTime
        };

        return cityData;
    }

    public async Task<List<DistrictsApiResponse>> GetCities()
    {
        var lang = language switch
        {
            Language.Hebrew => "heb",
            Language.Russian => "rus",
            Language.Arabic => "arb",
            Language.English => "eng",
            _ => "heb"
        };

        return await memoryCache.GetOrCreateAsync("cities", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
            var response = await _httpClient.GetAsync($"https://www.oref.org.il/districts/districts_{lang}.json");
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to fetch cities: {response.ReasonPhrase}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<DistrictsApiResponse>>(content);
        }) ?? throw new InvalidOperationException();
    }
}