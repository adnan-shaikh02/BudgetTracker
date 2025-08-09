namespace BudgetTracker.Api.DTO
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public required string FullName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; }
        public string Role { get; set; } = "User";
    }
}
