using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Models;

namespace HomeFrontCommandLibrary.Interfaces;

public interface IHomeFrontCommand
{
    Task<Alert> GetActiveAlert(Language language = Language.Hebrew);
    Task<List<AlertHistory>> GetAlertsHistory(Language language = Language.Hebrew);
    Task<City> GetCityByName(string name, Language language = Language.Hebrew);
    Task<Category> GetCategoryByName(string name, Language language = Language.Hebrew);
}