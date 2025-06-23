using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;

namespace HomeFrontCommandLibrary.Interfaces;

internal interface ICityService
{
    Task<City> GetCityByName(string cityName, Language language = Language.Hebrew);
    Task<List<DistrictsApiResponse>> GetCities(Language language = Language.Hebrew);
}