using BudgetTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Api.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            var add = await _context.SaveChangesAsync();
            if (add > 0)
            {
                var successMsg = "Category added successfully";
                return successMsg;
            }
            throw new Exception("Failed to add category.");
        }

        public Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid category ID", nameof(id));
            }
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            _context.Categories.Remove(category);
            var delete = _context.SaveChangesAsync();
            if (delete.Result > 0)
            {
                return Task.FromResult(true);
            }
            throw new Exception("Failed to delete category.");
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid category ID", nameof(id));
            }
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                throw new KeyNotFoundException($"Category with ID {id} not found.");
            }
            return category;
        }

        public Task<Category> UpdateAsync(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category), "Category cannot be null.");
            }
            if (category.Id <= 0)
            {
                throw new ArgumentException("Invalid category ID", nameof(category.Id));
            }
            _context.Categories.Update(category);
            var update = _context.SaveChangesAsync();
            if (update.Result > 0)
            {
                return Task.FromResult(category);
            }
            throw new Exception("Failed to update category.");
        }
    }
}
