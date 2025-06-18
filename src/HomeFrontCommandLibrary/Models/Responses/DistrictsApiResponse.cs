using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Models.Responses;

internal class DistrictsApiResponse
{
    [JsonProperty("areaid")]
    public int AreaId { get; set; }

    [JsonProperty("areaname")]
    public string AreaName { get; set; } = string.Empty;

    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("value")]
    public string Value { get; set; } = string.Empty;

    [JsonProperty("label")]
    public string Label { get; set; } = string.Empty;

    [JsonProperty("rashut")]
    public string Rashut { get; set; } = string.Empty;

    [JsonProperty("label_he")]
    public string LabelHebrew { get; set; } = string.Empty;

    [JsonProperty("migun_time")]
    public int MigunTime { get; set; }
}