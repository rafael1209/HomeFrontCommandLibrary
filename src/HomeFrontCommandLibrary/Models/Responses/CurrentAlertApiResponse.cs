using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Models.Responses;

public class CurrentAlertApiResponse
{
    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("cat")]
    public int? Cat { get; set; }

    [JsonProperty("title")]
    public string? Title { get; set; }

    [JsonProperty("data")]
    public required List<string> Data { get; set; }

    [JsonProperty("desc")]
    public string? Desc { get; set; }
}