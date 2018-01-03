﻿using System.Collections.Generic;
using Linguist.DataLayer.Model;

namespace Linguist.Services.Interfaces
{
    public interface ICategoriesService
    {
        bool AddCategory(Category category);

        bool EditCategory(Category category);

        bool RemoveCategory(Category category);

        IEnumerable<Category> GetCategories();
    }
}
