using CodePracticeTrackingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CodePracticeTrackingApp.Models
{
    public static class SeedData
    {
        private static TimeSpan GenerateRandomTimeSpan()
        {
            // Initialize the Random class
            Random random = new Random();

            // Generate a random number of minutes within the range [0, 60)
            int randomMinutes = random.Next(0, 60);

            // Create a TimeSpan using the random number of minutes
            TimeSpan randomTimeSpan = TimeSpan.FromMinutes(randomMinutes);
            return randomTimeSpan;
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
                        Timing = GenerateRandomTimeSpan(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Two Sum",
                        Difficulty = "Easy",
                        Frequency = 2,
                        Tag = "Hash Map",
                        Timing = GenerateRandomTimeSpan(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Maximum Subarray Sum",
                        Difficulty = "Medium",
                        Frequency = 1,
                        Tag = "Dynamic Programming",
                        Timing = GenerateRandomTimeSpan(),
                        LastUpdate = DateTime.Now,
                    },
                     new Problem
                     {
                         Title = "Alien Dictionary",
                         Difficulty = "Hard",
                         Frequency = 1,
                         Tag = "Topological Sort",
                         Timing = GenerateRandomTimeSpan(),
                         LastUpdate = DateTime.Now,
                     },
                    new Problem
                    {
                        Title = "Number of Provinces",
                        Difficulty = "Medium",
                        Frequency = 3,
                        Tag = "Disjoint Set",
                        Timing = GenerateRandomTimeSpan(),
                        LastUpdate = DateTime.Now,
                    }
                );;
                context.SaveChanges();
            }
        }
    }
}
