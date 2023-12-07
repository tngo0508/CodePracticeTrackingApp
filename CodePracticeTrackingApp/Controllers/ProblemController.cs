using CodePracticeTrackingApp.Data;
using CodePracticeTrackingApp.Models;
using CodePracticeTrackingApp.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            // Set the LicenseContext before using any EPPlus functionality
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial; // or LicenseContext.Commercial
        }

        public void SetSessionVm(DatabaseContext context)
        {
            var problemRecords = _databaseContext.Problems.ToList();
            sessionVm.Problems = problemRecords;
            sessionVm.hasData = problemRecords.Any();
        }
        public IActionResult Index()
        {
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
        private void ApplyRowColor(ExcelWorksheet worksheet, int row, string difficulty)
        {
            var color = System.Drawing.Color.White; // Default color

            switch (difficulty.ToLower())
            {
                case "easy":
                    color = System.Drawing.Color.LightGreen;
                    break;
                case "medium":
                    color = System.Drawing.Color.LightYellow;
                    break;
                case "hard":
                    color = System.Drawing.Color.LightPink;
                    break;
                    // Add more cases as needed
            }

            for (var i = 1; i <= worksheet.Dimension.End.Column; i++)
            {
                worksheet.Cells[row, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells[row, i].Style.Fill.BackgroundColor.SetColor(color);
            }
        }
        public ActionResult ExportToExcel()
        {
            var problems = _databaseContext.Problems.ToList();
            if (problems != null)
            {
                // Create Excel package
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Problems");

                    // Add headers
                    var headers = new List<string> { "Title", "Tag", "Frequency", "Difficulty", "Last Update", "Timing" };
                    for (var i = 0; i < headers.Count; i++)
                    {
                        var cell = worksheet.Cells[1, i + 1];
                        cell.Value = headers[i];
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // Populate Excel sheet with data
                    for (var i = 0; i < problems.Count; i++)
                    {
                        var problem = problems[i];
                        worksheet.Cells[i + 2, 1].Value = problem.Title;
                        worksheet.Cells[i + 2, 2].Value = problem.Tag;
                        worksheet.Cells[i + 2, 3].Value = problem.Frequency;
                        worksheet.Cells[i + 2, 4].Value = problem.Difficulty;
                        worksheet.Cells[i + 2, 5].Value = problem.LastUpdate.ToString("yyyy-MM-dd");
                        worksheet.Cells[i + 2, 6].Value = problem.Timing;

                        // Apply row color based on difficulty
                        ApplyRowColor(worksheet, i + 2, problem.Difficulty);
                    }

                    // Enable sorting for the entire worksheet
                    worksheet.Cells["A1:F1"].AutoFilter = true;

                    // Auto-expand columns
                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    // Create a bar chart based on frequency
                    var chart = worksheet.Drawings.AddChart("FrequencyBarChart", eChartType.BarClustered);
                    chart.SetPosition(0, 7, 0, 0);
                    chart.SetSize(600, 400);
                    chart.Title.Text = "Frequency Bar Chart";
                    chart.Series.Add(worksheet.Cells["C2:C" + (problems.Count + 1)], worksheet.Cells["A2:A" + (problems.Count + 1)]);

                    // Set content type and file name
                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = $"CodeTrack_Export_{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.xlsx";

                    // Convert Excel package to byte array
                    byte[] fileContents = package.GetAsByteArray();

                    // Return Excel file as response
                    return File(fileContents, contentType, fileName);
                }
            }
            return Json(new { error = true, message = "Cannot Export" });
        }

        [HttpPost]
        public IActionResult ImportFromExcel(IFormFile file)
        {
            try
            {
                // Clear existing records
                _databaseContext.Problems.RemoveRange(_databaseContext.Problems);
                _databaseContext.SaveChanges();

                if (file != null && file.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        file.CopyTo(stream);
                        using (var package = new ExcelPackage(stream))
                        {
                            var worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                // Assuming your model has properties like Title, Tag, Frequency, Difficulty, LastUpdate, Timing
                                var problem = new Problem
                                {
                                    Title = worksheet.Cells[row, 1].Value?.ToString(),
                                    Tag = worksheet.Cells[row, 2].Value?.ToString(),
                                    Frequency = int.Parse(worksheet.Cells[row, 3].Value?.ToString()),
                                    Difficulty = worksheet.Cells[row, 4].Value?.ToString(),
                                    LastUpdate = DateTime.Parse(worksheet.Cells[row, 5].Value?.ToString()),
                                    Timing = double.Parse(worksheet.Cells[row, 6].Value?.ToString())
                                };

                                _databaseContext.Problems.Add(problem);
                            }

                            _databaseContext.SaveChanges();
                        }
                    }
                }

                TempData["success"] = "Import successful";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Error during import: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
