using HomeFrontCommandLibrary.Models;
using HomeFrontCommandLibrary.Models.Responses;

namespace HomeFrontCommandLibrary.Interfaces;

public interface ICategoryService
{
    Task<Category> GetCategoryByName(string name);
}