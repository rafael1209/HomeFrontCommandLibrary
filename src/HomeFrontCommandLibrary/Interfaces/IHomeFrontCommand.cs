using HomeFrontCommandLibrary.Models;

namespace HomeFrontCommandLibrary.Interfaces;

public interface IHomeFrontCommand
{
    Task<Alert> GetActiveAlert();
    Task<List<AlertHistory>> GetAlertsHistory();
}