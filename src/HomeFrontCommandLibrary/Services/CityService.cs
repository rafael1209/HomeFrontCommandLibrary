using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Services;

internal class CityService(ICacheService cacheService) : ICityService
{
    private readonly HttpClient _httpClient = new();

    public async Task<City> GetCityByName(string cityName, Language language = Language.Hebrew)
    {
        var cites = await GetCities(language);

        var city = cites.FirstOrDefault(c => c.LabelHebrew.Equals(cityName, StringComparison.OrdinalIgnoreCase));

        if (city == null)
        {
            return new City
            {
                AreaId = -1,
                Name = cityName,
                AreaName = string.Empty,
                ProtectionTime = -1
            };
        }

        var cityData = new City
        {
            AreaId = city.AreaId,
            Name = city.Label,
            AreaName = city.AreaName,
            ProtectionTime = city.MigunTime
        };

        return cityData;
    }

    public async Task<List<DistrictsApiResponse>> GetCities(Language language = Language.Hebrew)
    {
        var langCode = language switch
        {
            Language.Hebrew => "heb",
            Language.Russian => "rus",
            Language.Arabic => "arb",
            Language.English => "eng",
            _ => "heb"
        };

        var cacheKey = $"cities-{langCode}";

        var cached = await cacheService.GetCachedData(cacheKey);
        if (cached is List<DistrictsApiResponse> cities)
            return cities;

        var response = await _httpClient.GetAsync($"https://www.oref.org.il/districts/districts_{langCode}.json");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch cities: {response.ReasonPhrase}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<DistrictsApiResponse>>(content)
                     ?? throw new InvalidOperationException();

        await cacheService.SetCachedData(cacheKey, result, TimeSpan.FromHours(1));

        return result;
    }

}