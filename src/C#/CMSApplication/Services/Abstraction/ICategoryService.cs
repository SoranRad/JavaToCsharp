using System.Collections.Generic;
using System.Runtime.InteropServices;
using CMSApplication.Data.Entity;

namespace CMSApplication.Services.Abstraction
{
    public interface ICategoryService
    {
        Task<Category> addCategory(Category category);

        Task<Category> updateCategory(Category category);

        Task<List<Category>> getCategories();

        Task<Category> getCategory(long categoryId);

        Task deleteCategory(long categoryId);

    }
}
