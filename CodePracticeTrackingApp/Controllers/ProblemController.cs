using CodePracticeTrackingApp.Data;
using CodePracticeTrackingApp.Models;
using CodePracticeTrackingApp.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Security.Claims;

namespace CodePracticeTrackingApp.Controllers
{
    [Authorize]
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
        private string GetCurrentUserId()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }

        public void SetSessionVm(DatabaseContext context, string userId)
        {
            var problemRecords = _databaseContext.Problems
                .Where(p => p.ApplicationUserId == userId)
                .ToList();

            sessionVm.Problems = problemRecords;
            sessionVm.hasData = problemRecords.Any();
        }
        public IActionResult Index()
        {
            var userId = GetCurrentUserId();
            SetSessionVm(_databaseContext, userId);
            return View(sessionVm);
        }
        [HttpGet]
        public IActionResult ProblemList()
        {
            try
            {
                var userId = GetCurrentUserId();
                var problems = _databaseContext.Problems
                .Where(p => p.ApplicationUserId == userId)
                .ToList();
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
            try
            {
                var userId = GetCurrentUserId();
                if (!_databaseContext.Problems.Where(p => p.ApplicationUserId == userId).Any())
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
                            ApplicationUserId = userId
                        },
                    new Problem
                    {
                        Title = "Two Sum",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Hash Map",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Maximum Subarray Sum",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Dynamic Programming",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                     new Problem
                     {
                         Title = "Alien Dictionary",
                         Difficulty = "Hard",
                         Frequency = SeedData.GenerateRandomFrequency(),
                         Tag = "Topological Sort",
                         Timing = SeedData.GenerateRandomTime(),
                         LastUpdate = SeedData.GenerateRandomDateTime(),
                         ApplicationUserId = userId
                     },
                    new Problem
                    {
                        Title = "Number of Provinces",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Disjoint Set",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Is Subsequence",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Two Pointers",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Move Zeroes",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Two Pointers",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Max Number of K-Sum Pairs",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Two Pointers",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Maximum Average Subarray I",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Sliding Window",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Maximum Number of Vowels in a Substring of Given Length",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Sliding Window",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Find the Highest Altitude",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Prefix Sum",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Find the Difference of Two Arrays",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Hash map",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Decode String",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Stack",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Asteroid Collision",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Stack",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Removing Stars From a String",
                        Difficulty = "Hard",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Stack",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Number of Recent Calls",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Queue",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Dota2 Senate",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Queue",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Delete the Middle Node of a Linked List",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Linked List",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Maximum Depth of Binary Tree",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "DFS",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Leaf-Similar Trees",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "DFS",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Binary Tree Right Side View",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "BFS",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Search in a Binary Tree",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Delete Node in a BST",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Counting Bits",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Bit Manipulation",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Search Suggestion System",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Trie",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Unique Paths",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "DP",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Combination Sum III",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Backtracking",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Guess Number Higher or Lower",
                        Difficulty = "Easy",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "Binary Search",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    },
                    new Problem
                    {
                        Title = "Rotting Oranges",
                        Difficulty = "Medium",
                        Frequency = SeedData.GenerateRandomFrequency(),
                        Tag = "BFS",
                        Timing = SeedData.GenerateRandomTime(),
                        LastUpdate = SeedData.GenerateRandomDateTime(),
                        ApplicationUserId = userId
                    }
                    );
                }
                _databaseContext.SaveChanges();
                SetSessionVm(_databaseContext, userId);
                TempData["success"] = "Seed Data successfully";
            }
            catch (Exception e)
            {
                TempData["error"] = "Seed Data failed.";
            }

            return View(nameof(Index), sessionVm);
        }


        [HttpPost]
        public IActionResult Upsert(ProblemVM problemVm)
        {
            try
            {
                problemVm.Problem.ApplicationUserId = GetCurrentUserId();
                if (ModelState.IsValid)
                {
                    var userId = GetCurrentUserId();
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
                    SetSessionVm(_databaseContext, userId);

                    return RedirectToAction(nameof(Index), sessionVm);
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see Thomas Ngo via tngo0508@gmail.com");
            }
            TempData["error"] = "There is an error. Please contact Thomas Ngo via tngo0508@gmail.com";
            return View(nameof(Upsert), problemVm);
        }

        [HttpGet]
        public IActionResult DeleteAll()
        {
            try
            {
                var userId = GetCurrentUserId();
                // Retrieve all records from the table
                var allRecords = _databaseContext.Problems
                    .Where(p => p.ApplicationUserId == userId)
                    .ToList();

                // Remove all records from the DbSet
                _databaseContext.Problems.RemoveRange(allRecords);

                // Save changes to the database
                _databaseContext.SaveChanges();
                var sessionVm = new SessionVM
                {
                    Problems = null,
                    hasData = false
                };
                TempData["success"] = "Delete all records successfully";

            }
            catch (Exception ex)
            {
                TempData["error"] = "Failed to delete all records";
            }
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
            problemVm.Problem.ApplicationUserId = GetCurrentUserId();
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
                var userId = GetCurrentUserId();
                var problem = _databaseContext.Problems.Find(id);
                if (problem == null)
                {
                    //return NotFound();
                    return Json(new { success = false, message = "Error while deleting" });
                }
                _databaseContext.Problems.Remove(problem);
                _databaseContext.SaveChanges();
                SetSessionVm(_databaseContext, userId);
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
            var color = System.Drawing.Color.White;

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
            }

            for (var i = 1; i <= worksheet.Dimension.End.Column; i++)
            {
                worksheet.Cells[row, i].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[row, i].Style.Fill.BackgroundColor.SetColor(color);
            }
        }

        private void SetHeaders(ExcelWorksheet worksheet, List<string> headers)
        {
            for (var i = 0; i < headers.Count; i++)
            {
                var cell = worksheet.Cells[1, i + 1];
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                cell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }
        }

        private void ApplyTableBorders(ExcelRange tableRange)
        {
            var tableBorder = tableRange.Style.Border;
            tableBorder.Top.Style = ExcelBorderStyle.Thin;
            tableBorder.Bottom.Style = ExcelBorderStyle.Thin;
            tableBorder.Left.Style = ExcelBorderStyle.Thin;
            tableBorder.Right.Style = ExcelBorderStyle.Thin;
        }

        private void FormatLastUpdateColumn(ExcelColumn column)
        {
            column.Style.Numberformat.Format = "yyyy-MM-dd hh:mm:ss";
        }

        private void EnableSorting(ExcelWorksheet worksheet, string address)
        {
            worksheet.Cells[address].AutoFilter = true;
        }

        private void AutoFitColumns(ExcelWorksheet worksheet)
        {
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }

        private void CreateBarChart(ExcelWorksheet chartWorksheet, string chartTitle, ExcelRangeBase seriesAddress, ExcelRangeBase categoryAddress, System.Drawing.Color barColor, string categoryString)
        {
            var chart = chartWorksheet.Drawings.AddChart(chartTitle, eChartType.BarClustered);
            chart.SetPosition(0, 7, 0, 0);

            // Calculate chart size based on the number of data points
            int chartHeight = Math.Max(400, seriesAddress.End.Row - seriesAddress.Start.Row + 1) * 2;
            chart.SetSize(600, chartHeight);

            chart.Title.Text = chartTitle;

            // Set chart style
            chart.Style = eChartStyle.Style26;

            // Configure series
            var series = chart.Series.Add(seriesAddress, categoryAddress);
            series.HeaderAddress = categoryAddress;

            // Configure axis titles
            chart.YAxis.Title.Text = "Problem";
            chart.XAxis.Title.Text = categoryString;

            // Format axis labels
            chart.XAxis.MajorTickMark = eAxisTickMark.None;
            chart.XAxis.MinorTickMark = eAxisTickMark.None;
            chart.XAxis.Title.Font.Size = 12;

            chart.YAxis.MajorTickMark = eAxisTickMark.None;
            chart.YAxis.MinorTickMark = eAxisTickMark.None;
            chart.YAxis.Title.Font.Size = 12;

            // Remove the legend
            chart.Legend.Remove();

            // Set bar color
            series.Fill.Color = barColor;
        }

        private void CreateRadarChart(ExcelWorksheet chartWorksheet, string chartTitle, ExcelRangeBase seriesAddress, ExcelRangeBase categoryAddress)
        {
            var chart = chartWorksheet.Drawings.AddChart(chartTitle, eChartType.Radar);
            chart.SetPosition(0, 7, 0, 0);

            // Set a fixed size for the radar chart
            chart.SetSize(600, 400);

            chart.Title.Text = chartTitle;

            // Configure series
            var series = chart.Series.Add(seriesAddress, categoryAddress);
            series.HeaderAddress = categoryAddress;

            // Remove the legend
            chart.Legend.Remove();
        }

        private void ExportDataToExcel(ExcelPackage package, List<Problem> problems)
        {
            var dataWorksheet = package.Workbook.Worksheets.Add("ProblemsData");
            var headers = new List<string> { "Title", "Tag", "Frequency", "Difficulty", "Last Update", "Timing" };
            SetHeaders(dataWorksheet, headers);

            for (var i = 0; i < problems.Count; i++)
            {
                var problem = problems[i];
                dataWorksheet.Cells[i + 2, 1].Value = problem.Title;
                dataWorksheet.Cells[i + 2, 2].Value = problem.Tag;
                dataWorksheet.Cells[i + 2, 3].Value = problem.Frequency;
                dataWorksheet.Cells[i + 2, 4].Value = problem.Difficulty;
                dataWorksheet.Cells[i + 2, 5].Value = problem.LastUpdate.ToString();
                dataWorksheet.Cells[i + 2, 6].Value = problem.Timing;

                ApplyRowColor(dataWorksheet, i + 2, problem.Difficulty);
            }

            var tableRange = dataWorksheet.Cells[dataWorksheet.Dimension.Address];
            ApplyTableBorders(tableRange);
            FormatLastUpdateColumn(dataWorksheet.Column(5));
            EnableSorting(dataWorksheet, "A1:F1");
            AutoFitColumns(dataWorksheet);
        }

        private void CreatePieChart(ExcelWorksheet chartWorksheet, string chartTitle, ExcelRangeBase seriesAddress, ExcelRangeBase categoryAddress)
        {
            // Add a new pie chart to the specified worksheet with the given chartTitle
            var chart = chartWorksheet.Drawings.AddChart(chartTitle, eChartType.Pie);

            // Set the position of the chart on the worksheet
            chart.SetPosition(6, 6, 8, 0);

            // Set a fixed size for the pie chart (width: 400, height: 400)
            chart.SetSize(400, 400);

            // Set the title of the pie chart
            chart.Title.Text = chartTitle;

            // Add a series to the pie chart manually using the specified seriesAddress
            var series = chart.Series.Add(seriesAddress.Address, categoryAddress.Address);
            series.HeaderAddress = categoryAddress;

            // Remove the legend
            //chart.Legend.Remove();
        }


        private void CreateDiffStatTable(ExcelPackage package, List<Problem> problems)
        {
            // Add Difficulty table to the first worksheet in column H
            var difficultyTableWorksheet = package.Workbook.Worksheets[0]; // Assuming the first worksheet is at index 0
            var difficultyTableStartCell = difficultyTableWorksheet.Cells["J1"]; // Start from cell H1
            var difficultyTableHeaders = difficultyTableWorksheet.Cells["J1:J4"]; // Assuming you have Easy, Medium, and Hard difficulties

            // Extract difficulty distribution data from the problems list
            var difficultyDistribution = problems
                .GroupBy(p => p.Difficulty)
                .Select(g => new { Difficulty = g.Key, Count = g.Count() })
                .ToList();

            // Create a table from the data
            var difficultyTableRange = difficultyTableWorksheet.Cells[difficultyTableStartCell.Start.Row, difficultyTableStartCell.Start.Column, difficultyTableStartCell.Start.Row + difficultyDistribution.Count, difficultyTableStartCell.Start.Column];
            var difficultyTable = difficultyTableWorksheet.Tables.Add(difficultyTableRange, "DifficultyTable");
            difficultyTable.ShowHeader = false;
            difficultyTable.TableStyle = TableStyles.None;

            // Format the table
            //difficultyTable.TableStyle = TableStyles.Medium2;

            // Add "Difficulty" column next to the table in column I
            var difficultyColumnStartCell = difficultyTableWorksheet.Cells["I1"]; // Start from cell I1
            difficultyColumnStartCell.Value = "Difficulty";
            for (int i = 0; i < difficultyDistribution.Count; i++)
            {
                difficultyColumnStartCell.Offset(i + 1, 0).Value = difficultyDistribution[i].Difficulty;
            }

            // Add "Count" column next to the table in column J
            var countColumnStartCell = difficultyTableWorksheet.Cells["J1"]; // Start from cell J1
            countColumnStartCell.Value = "Count";
            for (int i = 0; i < difficultyDistribution.Count; i++)
            {
                countColumnStartCell.Offset(i + 1, 0).Value = difficultyDistribution[i].Count;
            }


            // Add Pie chart to the first worksheet
            var pieChartWorksheet = package.Workbook.Worksheets[0]; // Assuming the first worksheet is at index 0

            // Prepare data for the chart based on the difficulty table
            var pieSeriesAddress = pieChartWorksheet.Cells["J2:J" + (difficultyDistribution.Count + 1)];
            var pieCategoryAddress = pieChartWorksheet.Cells["I2:I" + (difficultyDistribution.Count + 1)];

            // Create and insert the pie chart into column L
            CreatePieChart(pieChartWorksheet, "Difficulty Distribution", pieSeriesAddress, pieCategoryAddress);
            pieChartWorksheet.Row(1).Height = 20; // Adjust the height based on your preference

        }

        private void ExportChartsToExcel(ExcelPackage package, List<Problem> problems)
        {
            var dataWorksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first sheet

            // Add Frequency chart
            var frequencyChartWorksheet = package.Workbook.Worksheets.Add("FrequencyChart");
            var frequencySeriesAddress = dataWorksheet.Cells[2, 3, problems.Count + 1, 3];
            var frequencyCategoryAddress = dataWorksheet.Cells[2, 1, problems.Count + 1, 1];
            CreateBarChart(frequencyChartWorksheet, "Frequency Bar Chart", frequencySeriesAddress, frequencyCategoryAddress, System.Drawing.Color.LightBlue, "Frequency");

            // Add Timing chart
            var timingChartWorksheet = package.Workbook.Worksheets.Add("TimingChart");
            var timingSeriesAddress = dataWorksheet.Cells[2, 6, problems.Count + 1, 6];
            var timingCategoryAddress = dataWorksheet.Cells[2, 1, problems.Count + 1, 1];
            CreateBarChart(timingChartWorksheet, "Timing Bar Chart", timingSeriesAddress, timingCategoryAddress, System.Drawing.Color.LightPink, "Timing");

            // Add Radar chart
            var radarChartWorksheet = package.Workbook.Worksheets.Add("RadarChart");

            // Group problems by Tag and calculate the total frequency for each Tag
            var groupedProblems = problems.GroupBy(p => p.Tag)
                                          .Select(g => new { Tag = g.Key, TotalFrequency = g.Sum(p => p.Frequency) })
                                          .ToList();

            // Assuming "TotalFrequency" is the new column representing the total frequency for each Tag
            radarChartWorksheet.Cells["B1"].Value = "Tag";
            radarChartWorksheet.Cells["C1"].Value = "Total Frequency";

            for (int i = 0; i < groupedProblems.Count; i++)
            {
                radarChartWorksheet.Cells[i + 2, 2].Value = groupedProblems[i].Tag;
                radarChartWorksheet.Cells[i + 2, 3].Value = groupedProblems[i].TotalFrequency;
            }

            // Define the series and category addresses based on the new data
            var radarSeriesAddress = radarChartWorksheet.Cells[2, 3, groupedProblems.Count + 1, 3]; // Assuming "Total Frequency" is in column 3
            var radarCategoryAddress = radarChartWorksheet.Cells[2, 2, groupedProblems.Count + 1, 2]; // Assuming "Tag" is in column 2

            CreateRadarChart(radarChartWorksheet, "Radar Chart", radarSeriesAddress, radarCategoryAddress);

            // Create Difficulty Statistic Table
            CreateDiffStatTable(package, problems);
        }

        public ActionResult ExportToExcel()
        {
            var userId = GetCurrentUserId();
            var problems = _databaseContext.Problems
                .Where(p => p.ApplicationUserId == userId)
                .ToList();
            if (problems != null)
            {
                using (var package = new ExcelPackage())
                {
                    ExportDataToExcel(package, problems);
                    ExportChartsToExcel(package, problems);

                    var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    var fileName = $"CodeTrack_Export_{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.xlsx";
                    byte[] fileContents = package.GetAsByteArray();

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
