using System.Text.Json.Serialization;

namespace HomeFrontCommandLibrary.Models.Responses;

internal class AlertHistoryApiResponse
{
    [JsonPropertyName("alertDate")]
    public DateTime AlertDate { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public string Data { get; set; } = string.Empty;

    [JsonPropertyName("category")]
    public int Category { get; set; }
}