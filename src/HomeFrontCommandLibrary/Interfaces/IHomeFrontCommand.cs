using HomeFrontCommandLibrary.Models;

namespace HomeFrontCommandLibrary.Interfaces;

public interface IHomeFrontCommand
{
    Task<Alert> GetActiveAlert();
    Task<List<AlertHistory>> GetAlertsHistory();
    Task<City> GetCityByName(string name);
    Task<Category> GetCategoryByName(string name);
    Task<Alert> GetActiveAlertExample(string categoryName);
    List<City> GetAllCities();
}