using BudgetTracker.Api.DTO;
using BudgetTracker.Api.Models;
using BudgetTracker.Api.Repository;
using System.Text;

namespace BudgetTracker.Api.Services
{
    public class UsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<IEnumerable<UsersDto>> GetAllUsersAsync()
        {
            var result = await _usersRepository.GetAllAsync();
            return result.Select(MapToUsersDto);
        }
        public async Task<UsersDto> GetUserByIdAsync(int id)
        {
            var result = await _usersRepository.GetByIdAsync(id);
            return MapToUsersDto(result);
        }
        public async Task<string> AddUserAsync(CreateUserDto createUserDto)
        {
            var user = new Users
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                PasswordHash = Encoding.UTF8.GetBytes(createUserDto.Password),
                Role = createUserDto.Role,
                CreatedAt = DateTime.UtcNow
            };
            return await _usersRepository.AddAsync(user);
        }
        public async Task<string> UpdateUserAsync(UpdateUserDto updateUserDto)
        {
            var existingUser = await _usersRepository.GetByIdAsync(updateUserDto.Id);
            if (existingUser == null) return null;

            // Update only the changed fields
            existingUser.FullName = updateUserDto.FullName;
            existingUser.Email = updateUserDto.Email;

            if (!string.IsNullOrWhiteSpace(updateUserDto.Password))
            {
                existingUser.PasswordHash = Encoding.UTF8.GetBytes(updateUserDto.Password);
            }

            existingUser.Role = updateUserDto.Role;

            return await _usersRepository.UpdateAsync(existingUser);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _usersRepository.DeleteAsync(id);
        }

        public async Task<string> UserLoginAsync(UserLoginDto loginDto)
        {
            return await _usersRepository.UserLogin(loginDto);
        }

        private static UsersDto MapToUsersDto(Users user)
        {
            return new UsersDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PasswordHash = "**********",
                Role = user.Role,
                CreatedAt = user.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")
            };
        }
    }
}
