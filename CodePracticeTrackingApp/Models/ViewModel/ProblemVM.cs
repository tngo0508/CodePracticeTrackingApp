using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CodePracticeTrackingApp.Models.ViewModel
{
    public class ProblemVM
    {
        [BindProperty]
        public Problem Problem { get; set; } = new Problem();
        public IEnumerable<SelectListItem> DifficultyList { get; set; } = new List<SelectListItem>()
        {
            new() { Value = "Easy", Text = "Easy" },
            new() { Value = "Medium", Text = "Medium" },
            new() { Value = "Hard", Text = "Hard" }
        };

    }
}
