using HomeFrontCommandLibrary.Models;

namespace HomeFrontCommandLibrary.Interfaces;

internal interface ICategoryService
{
    Task<Category> GetCategoryByName(string name);
}