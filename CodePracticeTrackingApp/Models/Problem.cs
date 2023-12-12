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
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only alphabet is allowed")]
        public string Title { get; set; }
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only alphabet is allowed")]
        public string? Tag { get; set; }
        [DefaultValue(0)]
        public int Frequency { get; set; } = 0;
        [Required(ErrorMessage = "This field is required")]
        public string Difficulty { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Last Update")]
        public DateTime LastUpdate { get; set; }
        [DisplayName("Time (minutes)")]
        public double Timing { get; set; }

        // Navigation property for the user
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
