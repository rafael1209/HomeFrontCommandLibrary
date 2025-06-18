using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Interfaces;
using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace HomeFrontCommandLibrary.Services;

internal class CategoryService(IMemoryCache cache, Language language = Language.Hebrew) : ICategoryService
{
    private readonly HttpClient _httpClient = new();

    public async Task<Category> GetCategoryByName(string name)
    {
        var translations = await GetTranslations();

        var translation = translations.FirstOrDefault(t =>
                              string.Equals(t.HebTitle, name, StringComparison.OrdinalIgnoreCase))
                          ?? throw new Exception($"Translation not found for '{name}'");

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
        return await cache.GetOrCreateAsync("translations", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60);
            var response = await _httpClient.GetAsync("https://www.oref.org.il/alerts/alertsTranslation.json");
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to fetch translations: {response.ReasonPhrase}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AlertTranslationApiResponse>>(content);
        }) ?? throw new InvalidOperationException();
    }
}