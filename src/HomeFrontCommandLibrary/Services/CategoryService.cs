using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Services;

internal class CategoryService(ICacheService cacheService) : ICategoryService
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
                Title = new CategoryTitle { Hebrew = name },
                Description = new CategoryDescription()
            };
        }

        var category = new Category
        {
            Id = translation.CatId,
            MatrixId = translation.MatrixCatId,
            Title = new CategoryTitle
            {
                Hebrew = translation.HebTitle,
                Russian = translation.RusTitle,
                Arabic = translation.ArbTitle,
                English = translation.EngTitle
            },
            Description = new CategoryDescription
            {
                Hebrew = translation.Heb,
                Russian = translation.Rus,
                Arabic = translation.Arb,
                English = translation.Eng
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