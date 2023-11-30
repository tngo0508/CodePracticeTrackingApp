using CodePracticeTrackingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CodePracticeTrackingApp.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        public DbSet<Problem> Problems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Problem>().ToTable(nameof(Problem));
            //modelBuilder.Entity<Problem>().HasData(
            //    new { Id = 1, Title = "Two Sum" },
            //    new { Id = 2, Title = "Maximum Subarray"}) ;
        }
    }
}
