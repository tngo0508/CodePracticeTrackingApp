using CodePracticeTrackingApp.Data;
using CodePracticeTrackingApp.Dto;
using CodePracticeTrackingApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace CodePracticeTrackingApp.Controllers
{
    public class ProblemController : Controller
    {
        private readonly DatabaseContext _databaseContext;
        public ProblemController(DatabaseContext context)
        {
            _databaseContext = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ProblemList()
        {
            var problems = _databaseContext.Problems.ToList();
            var formattedProblems = problems.Select(x => new
            {
                id = x.Id,
                tag = x.Tag,
                title = x.Title,
                difficulty = x.Difficulty,
                frequency = x.Frequency,
                lastUpdate = x.LastUpdate.ToString("yyyy-MM-dd"),
            });
            return Json(new { data = formattedProblems });
        }

        [HttpPost]
        public IActionResult AddProblem(Problem problem)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _databaseContext.Add(problem);
                    _databaseContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction(nameof(Problem));
        }

        public IActionResult AddProblem()
        {
            return View();
        }
    }
}
