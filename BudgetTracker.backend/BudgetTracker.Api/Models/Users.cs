using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Api.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public required string FullName { get; set; }
        public required string Email { get; set; }
        public required byte[] PasswordHash { get; set; }
        public string Role { get; set; } = "User";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
