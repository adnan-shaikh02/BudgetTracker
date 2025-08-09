using BudgetTracker.Api.DTO;
using BudgetTracker.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BudgetTracker.Api.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDbContext _context;

        public UsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<string> AddAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            _context.Users.Add(user);
            var result = _context.SaveChangesAsync();
            if (result.Result > 0)
            {
                return Task.FromResult("User added successfully");
            }
            throw new Exception("Failed to add user.");
        }

        public Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(id));
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            _context.Users.Remove(user);
            var result = _context.SaveChangesAsync();
            if (result.Result > 0)
            {
                return Task.FromResult(true);
            }
            throw new Exception("Failed to delete user.");
        }

        public async Task<IEnumerable<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public Task<Users> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(id));
            }
            var user = _context.Users.Find(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return Task.FromResult(user);
        }

        public Task<string> UpdateAsync(Users user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }
            if (user.Id <= 0)
            {
                throw new ArgumentException("Invalid user ID", nameof(user.Id));
            }
            _context.Users.Update(user);
            var result = _context.SaveChangesAsync();
            if (result.Result > 0)
            {
                return Task.FromResult("User updated successfully");
            }
            throw new Exception("Failed to update user.");
        }

        public Task<string> UserLogin(UserLoginDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Email))
            {
                throw new ArgumentException("Email cannot be null or empty.", nameof(loginDto.Email));
            }
            if (string.IsNullOrWhiteSpace(loginDto.Password))
            {
                throw new ArgumentException("Password cannot be null or empty.", nameof(loginDto.Email));
            }
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email && u.PasswordHash == Encoding.UTF8.GetBytes(loginDto.Password));
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            return Task.FromResult("Login successful");
        }
    }
}
