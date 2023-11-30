using System.ComponentModel.DataAnnotations;

namespace CodePracticeTrackingApp.Dto
{
    public partial class ProblemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Tag { get; set; }
        public int? Frequency { get; set; }
        public string? Difficulty { get; set; }
        public string LastUpdate { get; set; }
    }
}
