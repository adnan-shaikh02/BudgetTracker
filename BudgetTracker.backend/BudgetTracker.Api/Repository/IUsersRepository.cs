using BudgetTracker.Api.Models;
using BudgetTracker.Api.DTO;

namespace BudgetTracker.Api.Repository
{
    public interface IUsersRepository
    {
        Task<string> AddAsync(Users user);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Users>> GetAllAsync();
        Task<Users> GetByIdAsync(int id);
        Task<string> UpdateAsync(Users user);
        Task<string> UserLogin(UserLoginDto loginDto);
    }
}
