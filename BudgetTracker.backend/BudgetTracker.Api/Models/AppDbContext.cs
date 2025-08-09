using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Api.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
