using CodePracticeTrackingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CodePracticeTrackingApp.Models
{
    public static class SeedData
    {
        private static double GenerateRandomTime()
        {
            // Initialize the Random class
            Random random = new Random();

            // Generate a random number of minutes within the range [0, 60)
            return random.Next(0, 60);
        }
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
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Two Sum",
                        Difficulty = "Easy",
                        Frequency = 2,
                        Tag = "Hash Map",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Maximum Subarray Sum",
                        Difficulty = "Medium",
                        Frequency = 1,
                        Tag = "Dynamic Programming",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                     new Problem
                     {
                         Title = "Alien Dictionary",
                         Difficulty = "Hard",
                         Frequency = 1,
                         Tag = "Topological Sort",
                         Timing = GenerateRandomTime(),
                         LastUpdate = DateTime.Now,
                     },
                    new Problem
                    {
                        Title = "Number of Provinces",
                        Difficulty = "Medium",
                        Frequency = 3,
                        Tag = "Disjoint Set",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    }
                );;
                context.SaveChanges();
            }
        }
    }
}
