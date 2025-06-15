using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models.Responses;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Services;

public class AlertService : IAlertService
{
    private readonly HttpClient _httpClient = new();

    public async Task<CurrentAlertApiResponse?> GetCurrentAlert()
    {
        var response = await _httpClient.GetAsync("https://www.oref.org.il/warningMessages/alert/Alerts.json");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch current alert: {response.ReasonPhrase}");

        var content = await response.Content.ReadAsStringAsync();

        var currentAlert = JsonConvert.DeserializeObject<CurrentAlertApiResponse>(content);

        return currentAlert;
    }

    public async Task<List<AlertHistoryApiResponse>> GetAlertsHistory()
    {
        var response = await _httpClient.GetAsync("https://www.oref.org.il/warningMessages/alert/History/AlertsHistory.json");

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch alerts history: {response.ReasonPhrase}");

        var content = await response.Content.ReadAsStringAsync();

        var alertsHistory = JsonConvert.DeserializeObject<List<AlertHistoryApiResponse>>(content);

        if (alertsHistory == null)
            throw new JsonException("Failed to deserialize alerts history response");

        return alertsHistory;
    }
}