using CodePracticeTrackingApp.Data;
using CodePracticeTrackingApp.Models;
using CodePracticeTrackingApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
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
            try
            {
                var problems = _databaseContext.Problems?.ToList();
                var formattedProblems = problems?.Select(x => new
                {
                    id = x.Id,
                    tag = x.Tag,
                    title = x.Title,
                    difficulty = x.Difficulty,
                    frequency = x.Frequency,
                    timing = x.Timing.ToString(),
                    lastUpdate = x.LastUpdate.ToString("yyyy-MM-dd hh:mm:ss"),
                });
                var maxFrequency = formattedProblems.Max(x => x.frequency);
                return Json(new { data = formattedProblems, maxFrequency });
            }
            catch (Exception ex)
            {
                return Json(new { data = Empty });
            }
        }



        [HttpPost]
        public IActionResult Upsert(ProblemVM problemVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    problemVm.Problem.LastUpdate = DateTime.Now;
                    if (problemVm.Problem.Id == 0)
                    {
                        // new problem is added
                        _databaseContext.Add(problemVm.Problem);
                    }
                    else
                    {
                        _databaseContext.Update(problemVm.Problem);
                    }
                    _databaseContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(nameof(Upsert), problemVm);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                // user adds new problem to the table
                return View(new ProblemVM());
            }

            // user updates a problem
            var query = _databaseContext.Problems.Where(x => x.Id == id);
            var problemVm = new ProblemVM { Problem = query.FirstOrDefault() };
            problemVm.Problem.Id = (int)id;
            return View(problemVm);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("id is null");
            }
            var problemFromDb = _databaseContext.Problems.Find(id);
            if (problemFromDb == null)
            {
                return NotFound();
            }
            return View(problemFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteProblem(int? id)
        {
            try
            {
                var problem = _databaseContext.Problems.Find(id);
                if (problem == null)
                {
                    return NotFound();
                }
                _databaseContext.Problems.Remove(problem);
                _databaseContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
