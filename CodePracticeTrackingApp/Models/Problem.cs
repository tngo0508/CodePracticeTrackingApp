using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;

namespace CodePracticeTrackingApp.Models
{
    public class Problem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only alphabet is allowed")]
        public string Title { get; set; }
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only alphabet is allowed")]
        public string? Tag { get; set; }
        public int? Frequency { get; set; }
        public string? Difficulty { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Last Update")]
        public DateTime LastUpdate { get; set; }
    }
}
