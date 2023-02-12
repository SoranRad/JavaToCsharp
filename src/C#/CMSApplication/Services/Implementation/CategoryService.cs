using CMSApplication.Data;
using CMSApplication.Data.Entity;
using CMSApplication.Services.Abstraction; 
using Microsoft.EntityFrameworkCore;

namespace CMSApplication.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly DBContext _context;

        public CategoryService(DBContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Category> addCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> updateCategory(Category category)
        {
            _context.Attach(category);
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<List<Category>> getCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> getCategory(long categoryId)
        {
            var obj = await _context.Categories.Where(x => x.Id == categoryId).FirstOrDefaultAsync();
            if (obj == null) throw new Exception("Category does not exist");
            return obj;
        }

        public async Task deleteCategory(long categoryId)
        {
            var category = new Category { Id = categoryId};
            _context.Entry(category).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
    }
}
