using CodePracticeTrackingApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CodePracticeTrackingApp.Models
{
    public static class SeedData
    {
        private static int GenerateRandomFrequency()
        {
            var random = new Random();
            return random.Next(0, 10);
        }
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
                if (context.Problems.Any())
                {
                    return;   // DB has been seeded
                }
                context.Problems.AddRange(
                    new Problem
                    {
                        Title = "Subtree of another subtree",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Tree",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Two Sum",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Hash Map",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Maximum Subarray Sum",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Dynamic Programming",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                     new Problem
                     {
                         Title = "Alien Dictionary",
                         Difficulty = "Hard",
                         Frequency = GenerateRandomFrequency(),
                         Tag = "Topological Sort",
                         Timing = GenerateRandomTime(),
                         LastUpdate = DateTime.Now,
                     },
                    new Problem
                    {
                        Title = "Number of Provinces",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Disjoint Set",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Is Subsequence",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Two Pointers",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Move Zeroes",
                        Difficulty = "EAsy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Two Pointer",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Max Number of K-Sum Pairs",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "tag",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Maximum Average Subarray I",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Sliding Window",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Maximum Number of Vowels in a Substring of Given Length",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Sliding Window",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Find the Highest Altitude",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Prefix Sum",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Find the Difference of Two Arrays",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Hash map",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Decode String",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Stack",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Asteroid Collision",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Stack",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Removing Stars From a String",
                        Difficulty = "diff",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Stack",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Number of Recent Calls",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Queue",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Dota2 Senate",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Queue",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Delete the Middle Node of a Linked List",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Linked List",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Maximum Depth of Binary Tree",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "DFS",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Leaf-Similar Trees",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "DFS",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Binary Tree Right Side View",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "BFS",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Search in a Binary Tree",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Delete Node in a BST",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Counting Bits",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Bit Manipulation",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Search Suggestion System",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Trie",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Unique Paths",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "DP",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Combination Sum III",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Backtracking",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Guess Number Higher or Lower",
                        Difficulty = "Easy",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "Binary Search",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    },
                    new Problem
                    {
                        Title = "Rotting Oranges",
                        Difficulty = "Medium",
                        Frequency = GenerateRandomFrequency(),
                        Tag = "BFS",
                        Timing = GenerateRandomTime(),
                        LastUpdate = DateTime.Now,
                    }
                );;
                context.SaveChanges();
            }
        }
    }
}
