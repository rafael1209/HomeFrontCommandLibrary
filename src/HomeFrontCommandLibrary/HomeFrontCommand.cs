using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Services;
using Microsoft.Extensions.Caching.Memory;

namespace HomeFrontCommandLibrary;

public class HomeFrontCommand(Language language = Language.Hebrew) : IHomeFrontCommand
{
    private static readonly IMemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions());
    private readonly IAlertService _alertService = new AlertService();
    private readonly ICategoryService _categoryService = new CategoryService(MemoryCache, language);
    private readonly ICityService _cityService = new CityService(MemoryCache, language);

    public async Task<Alert> GetActiveAlert()
    {
        var activeAlerts = await _alertService.GetCurrentAlert();

        if (activeAlerts == null)
        {
            return new Alert
            {
                Category = new Category(),
                Cities = [],
                AlertDate = DateTime.Now,
            };
        }
        var cities = new List<City>();
        foreach (var city in activeAlerts.Data)
        {
            cities.Add(await _cityService.GetCityByName(city));
        }

        var alerts = new Alert
        {
            Category = await _categoryService.GetCategoryByName(activeAlerts.Title),
            Cities = cities,
            AlertDate = DateTime.Now
        };

        return alerts;
    }

    public async Task<List<Alert>> GetAlertsHistory()
    {
        var alertsHistory = await _alertService.GetAlertsHistory();

        var alerts = new List<Alert>();
        foreach (var alert in alertsHistory)
        {
            alerts.Add(new Alert
            {
                Category = await _categoryService.GetCategoryByName(alert.Title),
                Cities = [await _cityService.GetCityByName(alert.Data)],
                AlertDate = alert.AlertDate,
            });
        }

        return alerts;
    }
}