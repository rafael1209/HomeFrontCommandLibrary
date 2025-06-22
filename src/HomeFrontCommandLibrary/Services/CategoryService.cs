using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Services;

internal class CategoryService(ICacheService cacheService, Language language = Language.Hebrew) : ICategoryService
{
    private readonly HttpClient _httpClient = new();

    public async Task<Category> GetCategoryByName(string name)
    {
        var translations = await GetTranslations();

        var translation = translations.FirstOrDefault(t =>
            string.Equals(t.HebTitle, name, StringComparison.OrdinalIgnoreCase));

        if (translation == null)
        {
            return new Category
            {
                Id = 0,
                MatrixId = 0,
                Title = name,
                Description = null
            };
        }

        var category = new Category
        {
            Id = translation.CatId,
            MatrixId = translation.MatrixCatId,
            Title = language switch
            {
                Language.Hebrew => translation.HebTitle,
                Language.Russian => translation.RusTitle,
                Language.Arabic => translation.ArbTitle,
                Language.English => translation.EngTitle,
                _ => translation.HebTitle
            },
            Description = language switch
            {
                Language.Hebrew => translation.Heb,
                Language.Russian => translation.Rus,
                Language.Arabic => translation.Arb,
                Language.English => translation.Eng,
                _ => translation.Heb
            }
        };

        return category;
    }

    private async Task<List<AlertTranslationApiResponse>> GetTranslations()
    {
        const string cacheKey = "translations";

        var cached = await cacheService.GetCachedData(cacheKey);
        if (cached is List<AlertTranslationApiResponse> translations)
            return translations;

        var response = await _httpClient.GetAsync("https://www.oref.org.il/alerts/alertsTranslation.json");
        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"Failed to fetch translations: {response.ReasonPhrase}");

        var content = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<List<AlertTranslationApiResponse>>(content)
                     ?? throw new InvalidOperationException("Failed to deserialize translations");

        await cacheService.SetCachedData(cacheKey, result, TimeSpan.FromHours(1));

        return result;
    }

}