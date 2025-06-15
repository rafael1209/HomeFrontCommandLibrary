using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Models.Responses;

public class AlertTranslationApiResponse
{
    [JsonProperty("heb")]
    public string Heb { get; set; } = string.Empty;

    [JsonProperty("eng")]
    public string Eng { get; set; } = string.Empty;

    [JsonProperty("rus")]
    public string Rus { get; set; } = string.Empty;

    [JsonProperty("arb")]
    public string Arb { get; set; } = string.Empty;

    [JsonProperty("catId")]
    public int CatId { get; set; }

    [JsonProperty("matrixCatId")]
    public int MatrixCatId { get; set; }

    [JsonProperty("hebTitle")]
    public string HebTitle { get; set; } = string.Empty;

    [JsonProperty("engTitle")]
    public string EngTitle { get; set; } = string.Empty;

    [JsonProperty("rusTitle")]
    public string RusTitle { get; set; } = string.Empty;

    [JsonProperty("arbTitle")]
    public string ArbTitle { get; set; } = string.Empty;

    [JsonProperty("updateType")]
    public string UpdateType { get; set; } = string.Empty;
}