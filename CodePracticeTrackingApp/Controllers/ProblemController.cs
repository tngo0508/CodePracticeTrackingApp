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
        public SessionVM sessionVm { get; set; }
        public ProblemController(DatabaseContext context)
        {
            _databaseContext = context;
            sessionVm = new SessionVM();
        }

        public void SetSessionVm(DatabaseContext context)
        {
            var problemRecords = _databaseContext.Problems.ToList();
            sessionVm.Problems = problemRecords;
            sessionVm.hasData = problemRecords.Any();
        }
        public IActionResult Index()
        {
            var allRecords = _databaseContext.Problems.ToList();
            SetSessionVm(_databaseContext);
            return View(sessionVm);
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
                    timing = x.Timing,
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

        [HttpGet]
        public IActionResult CreateRandomData()
        {
            if (!_databaseContext.Problems.Any())
            {
                _databaseContext.Problems.AddRange(
                    new Problem
                    {
                        Title = "Subtree of another subtree",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Tree",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                    },
                    new Problem
                    {
                        Title = "Two Sum",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Hash Map",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                    },
                    new Problem
                    {
                        Title = "Maximum Subarray Sum",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Dynamic Programming",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                    },
                    new Problem
                    {
                        Title = "Alien Dictionary",
                        Difficulty = "Hard",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Topological Sort",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                    },
                    new Problem
                    {
                        Title = "Number of Provinces",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Disjoint Set",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                    }
                );
            }
            _databaseContext.SaveChanges();
            SetSessionVm(_databaseContext);
            return View(nameof(Index), sessionVm);
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
                        TempData["success"] = "Problem is added successfully";
                    }
                    else
                    {
                        _databaseContext.Update(problemVm.Problem);
                        TempData["success"] = "Problem is updated successfully";
                    }
                    _databaseContext.SaveChanges();
                    SetSessionVm(_databaseContext);

                    return RedirectToAction(nameof(Index), sessionVm);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(nameof(Upsert), problemVm);
        }

        [HttpGet]
        public IActionResult DeleteAll()
        {
            // Retrieve all records from the table
            var allRecords = _databaseContext.Problems.ToList();

            // Remove all records from the DbSet
            _databaseContext.Problems.RemoveRange(allRecords);

            // Save changes to the database
            _databaseContext.SaveChanges();
            var sessionVm = new SessionVM
            {
                Problems = null,
                hasData = false
            };
            return RedirectToAction(nameof(Index), sessionVm);
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
        [HttpDelete, ActionName("Delete")]
        public IActionResult DeleteProblem(int? id)
        {
            try
            {
                var problem = _databaseContext.Problems.Find(id);
                if (problem == null)
                {
                    //return NotFound();
                    return Json(new { success = false, message = "Error while deleting" });
                }
                _databaseContext.Problems.Remove(problem);
                _databaseContext.SaveChanges();
                SetSessionVm(_databaseContext);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }

            //return RedirectToAction(nameof(Index), sessionVm);
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
