using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodePracticeTrackingApp.Models.ViewModel
{
    public class ProblemVM
    {
        public Problem Problem { get; set; }
        public IEnumerable<SelectListItem> DifficultyList { get; set; } = new List<SelectListItem>()
        {
            new() { Value = "easy", Text = "Easy" },
            new() { Value = "medium", Text = "Medium" },
            new() { Value = "hard", Text = "Hard" }
        };
    }
}
