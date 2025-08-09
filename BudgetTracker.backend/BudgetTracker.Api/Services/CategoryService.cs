using BudgetTracker.Api.Models;
using BudgetTracker.Api.Repository;

namespace BudgetTracker.Api.Services
{
    public class CategoryService
    {
        private readonly ICategoryRepository  _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task<string> AddCategoryAsync(Category category)
        {
            return await _categoryRepository.AddAsync(category);
        }
        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            return await _categoryRepository.UpdateAsync(category);
        }
        public async Task<bool> DeleteCategoryAsync(int id)
        {
            return await _categoryRepository.DeleteAsync(id);
        }
    }
}
