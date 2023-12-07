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
                    // Add a worksheet for the data
                    var dataWorksheet = package.Workbook.Worksheets.Add("ProblemsData");

                    // Add headers
                    var headers = new List<string> { "Title", "Tag", "Frequency", "Difficulty", "Last Update", "Timing" };
                    for (var i = 0; i < headers.Count; i++)
                    {
                        var cell = dataWorksheet.Cells[1, i + 1];
                        cell.Value = headers[i];
                        cell.Style.Font.Bold = true;
                        cell.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }

                    // Populate data worksheet with data
                    for (var i = 0; i < problems.Count; i++)
                    {
                        var problem = problems[i];
                        dataWorksheet.Cells[i + 2, 1].Value = problem.Title;
                        dataWorksheet.Cells[i + 2, 2].Value = problem.Tag;
                        dataWorksheet.Cells[i + 2, 3].Value = problem.Frequency;
                        dataWorksheet.Cells[i + 2, 4].Value = problem.Difficulty;
                        dataWorksheet.Cells[i + 2, 5].Value = problem.LastUpdate.ToString();
                        dataWorksheet.Cells[i + 2, 6].Value = problem.Timing;

                        // Apply row color based on difficulty
                        ApplyRowColor(dataWorksheet, i + 2, problem.Difficulty);
                    }

                    // Apply borders to the entire table
                    var tableRange = dataWorksheet.Cells[dataWorksheet.Dimension.Address];
                    var tableBorder = tableRange.Style.Border;
                    tableBorder.Top.Style = ExcelBorderStyle.Thin;
                    tableBorder.Bottom.Style = ExcelBorderStyle.Thin;
                    tableBorder.Left.Style = ExcelBorderStyle.Thin;
                    tableBorder.Right.Style = ExcelBorderStyle.Thin;

                    // Format "Last Update" column as DateTime
                    dataWorksheet.Column(5).Style.Numberformat.Format = "yyyy-MM-dd hh:mm:ss";

                    // Enable sorting for the data worksheet
                    dataWorksheet.Cells["A1:F1"].AutoFilter = true;

                    // Auto-expand columns for the data worksheet
                    dataWorksheet.Cells[dataWorksheet.Dimension.Address].AutoFitColumns();

                    // Add a new worksheet for the chart
                    var chartWorksheet = package.Workbook.Worksheets.Add("FrequencyChart");

                    // Create a bar chart based on frequency in the chart worksheet
                    var chart = chartWorksheet.Drawings.AddChart("FrequencyBarChart", eChartType.BarClustered);
                    chart.SetPosition(0, 7, 0, 0);

                    // Calculate the height based on the number of data points
                    var chartHeight = Math.Max(400, problems.Count * 20); // Adjust 20 based on your preference
                    chart.SetSize(600, chartHeight);

                    chart.Title.Text = "Frequency Bar Chart";
                    chart.Series.Add(dataWorksheet.Cells["C2:C" + (problems.Count + 1)], dataWorksheet.Cells["A2:A" + (problems.Count + 1)]);

                    // Add a new worksheet for the chart based on Timing
                    var timingChartWorksheet = package.Workbook.Worksheets.Add("TimingChart");

                    // Create a bar chart based on Timing in the chart worksheet
                    var timingChart = timingChartWorksheet.Drawings.AddChart("TimingBarChart", eChartType.BarClustered);
                    timingChart.SetPosition(0, 7, 0, 0);

                    // Calculate the height based on the number of data points
                    chartHeight = Math.Max(400, problems.Count * 20); // Adjust 20 based on your preference
                    timingChart.SetSize(600, chartHeight);

                    timingChart.Title.Text = "Timing Bar Chart";
                    timingChart.Series.Add(dataWorksheet.Cells["F2:F" + (problems.Count + 1)], dataWorksheet.Cells["A2:A" + (problems.Count + 1)]);

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

        private string ParseDifficulty(string difficulty)
        {
            // Handle 'Easy' or other non-numeric values
            switch (difficulty?.ToLower())
            {
                case "easy":
                    return "Easy"; // Or assign a numeric representation or handle it differently
                case "medium":
                    return "Medium"; // Or assign a numeric representation or handle it differently
                case "hard":
                    return "Hard"; // Or assign a numeric representation or handle it differently
                default:
                    return "Unknown"; // Or assign a default value or handle it differently
            }
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
                            var problemList = new List<Problem>();

                            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                            {
                                problemList.Add(new Problem
                                {
                                    // Parsing and handling empty string for Title
                                    Title = string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Value?.ToString()) ? string.Empty : worksheet.Cells[row, 1].Value?.ToString(),
                                    Tag = worksheet.Cells[row, 2].Value?.ToString(),

                                    // Parsing Frequency with int.TryParse
                                    Frequency = int.TryParse(worksheet.Cells[row, 3].Value?.ToString(), out var frequency) ? frequency : 0,

                                    // Parsing Difficulty
                                    Difficulty = ParseDifficulty(worksheet.Cells[row, 4].Value?.ToString()),

                                    // Parsing LastUpdate with DateTime.TryParse
                                    LastUpdate = DateTime.TryParse(worksheet.Cells[row, 5].Value?.ToString(), out var lastUpdate) ? lastUpdate : DateTime.Now,

                                    // Parsing Timing with double.TryParse
                                    Timing = double.TryParse(worksheet.Cells[row, 6].Value?.ToString(), out var timing) ? timing : 0.0
                                });
                            };
                            _databaseContext.Problems.AddRange(problemList);

                        }

                        _databaseContext.SaveChanges();
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
