using CodePracticeTrackingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CodePracticeTrackingApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DatabaseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<DatabaseContext>>()))
            {
                // Look for any movies.
                if (context.Problems.Any())
                {
                    return;   // DB has been seeded
                }
                context.Problems.AddRange(
                    new Problem
                    {
                        Title = "Subtree of another subtree",
                        Difficulty = "Easy",
                        Frequency = 1,
                        Tag = "Tree",
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Two Sum",
                        Difficulty = "Easy",
                        Frequency = 2,
                        Tag = "Hash Map",
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Maximum Subarray Sum",
                        Difficulty = "Medium",
                        Frequency = 1,
                        Tag = "Dynamic Programming",
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Number of Provinces",
                        Difficulty = "Medium",
                        Frequency = 3,
                        Tag = "Disjoint Set",
                        LastUpdate = DateTime.Now,
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
