using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Services;

internal class CityService: ICityService
{
    private readonly HttpClient _httpClient = new();
    private readonly ICacheService _cacheService;
    private static List<City> _citiesCache = [];
    private static readonly SemaphoreSlim _cacheLock = new(1, 1);
    private static bool _isInitialized;

    public CityService(ICacheService cacheService)
    {
        _cacheService = cacheService;
        _ = Init();
    }

    public async Task Init()
    {
        await LoadAllCities();
    }

    private async Task LoadAllCities()
    {
        if (_isInitialized)
            return;

        await _cacheLock.WaitAsync();
        try
        {
            if (_isInitialized)
                return;

            var hebTask = FetchCities("heb");
            var rusTask = FetchCities("rus");
            var arbTask = FetchCities("arb");
            var engTask = FetchCities("eng");

            await Task.WhenAll(hebTask, rusTask, arbTask, engTask);

            var hebCities = await hebTask;
            var rusCities = await rusTask;
            var arbCities = await arbTask;
            var engCities = await engTask;

            var cities = hebCities.Select(heb =>
            {
                var rus = rusCities.FirstOrDefault(c => c.Id == heb.Id);
                var arb = arbCities.FirstOrDefault(c => c.Id == heb.Id);
                var eng = engCities.FirstOrDefault(c => c.Id == heb.Id);

                return new City
                {
                    Id = int.TryParse(heb.Id, out var id) ? id : 0,
                    AreaId = heb.AreaId,
                    AreaName = heb.AreaName,
                    ProtectionTime = heb.MigunTime,
                    Name = new CityName
                    {
                        Hebrew = heb.Label,
                        Russian = rus?.Label ?? heb.Label,
                        Arabic = arb?.Label ?? heb.Label,
                        English = eng?.Label ?? heb.Label
                    },
                    Reshut = new ReshutName
                    {
                        Hebrew = heb.Rashut,
                        Russian = rus?.Rashut ?? heb.Rashut,
                        Arabic = arb?.Rashut ?? heb.Rashut,
                        English = eng?.Rashut ?? heb.Rashut
                    }
                };
            }).ToList();

            _citiesCache = cities;
            _isInitialized = true;
        }
        finally
        {
            _cacheLock.Release();
        }
    }

    private async Task<List<DistrictsApiResponse>> FetchCities(string langCode)
    {
        var response = await _httpClient.GetAsync($"https://www.oref.org.il/districts/districts_{langCode}.json");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch cities ({langCode}): {response.ReasonPhrase}");

        var content = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<List<DistrictsApiResponse>>(content)
               ?? throw new InvalidOperationException();
    }

    public async Task<City> GetCityByName(string cityName)
    {
        if (!_isInitialized)
            await LoadAllCities();

        var city = _citiesCache.FirstOrDefault(c =>
            c.Name.Hebrew.Equals(cityName, StringComparison.OrdinalIgnoreCase));

        if (city == null)
        {
            return new City
            {
                Id = -1,
                AreaId = -1,
                Name = new CityName { Hebrew = cityName },
                Reshut = new ReshutName(),
                AreaName = string.Empty,
                ProtectionTime = -1
            };
        }

        return city;
    }

    public async Task<List<DistrictsApiResponse>> GetCities()
    {
        if (!_isInitialized)
            await LoadAllCities();

        return _citiesCache.Select(c => new DistrictsApiResponse
        {
            Id = c.Id.ToString(),
            AreaId = c.AreaId,
            AreaName = c.AreaName,
            MigunTime = c.ProtectionTime,
            LabelHebrew = c.Name.Hebrew,
            Rashut = c.Reshut.Hebrew,
            Label = c.Name.Hebrew
        }).ToList();
    }

    public List<City> GetAllCities()
    {
        return _citiesCache;
    }
}