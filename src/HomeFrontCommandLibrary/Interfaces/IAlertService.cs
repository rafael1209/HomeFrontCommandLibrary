using HomeFrontCommandLibrary.Models.Responses;

namespace HomeFrontCommandLibrary.Interfaces;

internal interface IAlertService
{
    Task<CurrentAlertApiResponse?> GetCurrentAlert();
    Task<List<AlertHistoryApiResponse>> GetAlertsHistory();
}