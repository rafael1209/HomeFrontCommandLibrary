using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Services;

namespace HomeFrontCommandLibrary;

public class HomeFrontCommand : IHomeFrontCommand
{
    private static readonly ICacheService MemoryCache = new CacheService();
    private readonly IAlertService _alertService = new AlertService();
    private readonly ICategoryService _categoryService = new CategoryService(MemoryCache);
    private readonly ICityService _cityService = new CityService(MemoryCache);

    public async Task<Alert> GetActiveAlert(Language language = Language.Hebrew)
    {
        var activeAlerts = await _alertService.GetCurrentAlert();

        if (activeAlerts?.Title == null)
        {
            return new Alert
            {
                Category = null,
                Cities = null,
                AlertDate = DateTime.Now,
            };
        }

        var cities = await Task.WhenAll(
            activeAlerts.Data.Select(name => _cityService.GetCityByName(name, language))
        );

        var alert = new Alert
        {
            Category = await _categoryService.GetCategoryByName(activeAlerts.Title, language),
            Cities = cities.ToList(),
            AlertDate = DateTime.Now
        };

        return alert;
    }

    public async Task<List<AlertHistory>> GetAlertsHistory(Language language = Language.Hebrew)
    {
        var alertsHistory = await _alertService.GetAlertsHistory();

        var alerts = await Task.WhenAll(alertsHistory.Select(async alert =>
            new AlertHistory
            {
                Category = await _categoryService.GetCategoryByName(alert.Title, language),
                City = await _cityService.GetCityByName(alert.Data, language),
                AlertDate = alert.AlertDate
            }
        ));

        return alerts.ToList();
    }

    public async Task<City> GetCityByName(string name, Language language = Language.Hebrew)
    {
        var city = await _cityService.GetCityByName(name, language);

        return city;
    }

    public async Task<Category> GetCategoryByName(string name, Language language = Language.Hebrew)
    {
        var category = await _categoryService.GetCategoryByName(name, language);

        return category;
    }
}