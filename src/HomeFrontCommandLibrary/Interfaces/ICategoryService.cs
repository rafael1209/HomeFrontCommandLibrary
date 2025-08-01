﻿using HomeFrontCommandLibrary.Enums;
using HomeFrontCommandLibrary.Models;

namespace HomeFrontCommandLibrary.Interfaces;

internal interface ICategoryService
{
    Task<Category> GetCategoryByName(string name, Language language = Language.Hebrew);
}