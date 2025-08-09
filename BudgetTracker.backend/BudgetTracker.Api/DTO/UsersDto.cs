namespace BudgetTracker.Api.DTO
{
    public class UsersDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = "**********";
        public string Role { get; set; } = "User";
        public string CreatedAt { get; set; } = string.Empty;
    }
}
