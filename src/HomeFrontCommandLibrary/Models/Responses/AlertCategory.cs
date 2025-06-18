using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Models.Responses;

internal class AlertCategory
{
    [JsonProperty("id")]
    public int Id { get; set; }
    [JsonProperty("category")]
    public string Name { get; set; } = string.Empty;
    [JsonProperty("matrix_id")]
    public int MatrixId { get; set; }
    [JsonProperty("priority")]
    public int Priority { get; set; }
    [JsonProperty("queue")]
    public bool Queue { get; set; }
}