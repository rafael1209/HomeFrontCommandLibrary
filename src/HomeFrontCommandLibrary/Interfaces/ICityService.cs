using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;

namespace HomeFrontCommandLibrary.Interfaces;

internal interface ICityService
{
    Task<City> GetCityByName(string cityName);
    Task<List<DistrictsApiResponse>> GetCities();
}