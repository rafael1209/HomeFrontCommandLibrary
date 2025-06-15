using HomeFrontCommandLibrary.Models.Responses;

namespace HomeFrontCommandLibrary.Interfaces;

public interface IAlertService
{
    Task<CurrentAlertApiResponse?> GetCurrentAlert();
    Task<List<AlertHistoryApiResponse>> GetAlertsHistory();
}