using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Services;

namespace HomeFrontCommandLibrary;

public class HomeFrontCommand(Language language = Language.Hebrew) : IHomeFrontCommand
{
    private static readonly ICacheService MemoryCache = new CacheService();
    private readonly IAlertService _alertService = new AlertService();
    private readonly ICategoryService _categoryService = new CategoryService(MemoryCache, language);
    private readonly ICityService _cityService = new CityService(MemoryCache, language);

    public async Task<Alert> GetActiveAlert()
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
            activeAlerts.Data.Select(name => _cityService.GetCityByName(name))
        );

        var alert = new Alert
        {
            Category = await _categoryService.GetCategoryByName(activeAlerts.Title),
            Cities = cities.ToList(),
            AlertDate = DateTime.Now
        };

        return alert;
    }

    public async Task<List<AlertHistory>> GetAlertsHistory()
    {
        var alertsHistory = await _alertService.GetAlertsHistory();

        var alerts = await Task.WhenAll(alertsHistory.Select(async alert =>
            new AlertHistory
            {
                Category = await _categoryService.GetCategoryByName(alert.Title),
                City = await _cityService.GetCityByName(alert.Data),
                AlertDate = alert.AlertDate
            }
        ));

        return alerts.ToList();
    }
}