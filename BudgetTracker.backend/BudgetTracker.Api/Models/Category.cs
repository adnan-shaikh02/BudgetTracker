using System.ComponentModel.DataAnnotations;

namespace BudgetTracker.Api.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public required string Name { get; set; }
        public int? UserId { get; set; }
        public bool IsGlobal { get; set; } = false;
    }
}
