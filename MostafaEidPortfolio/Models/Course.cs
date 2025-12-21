// In Models/Course.cs

using System.ComponentModel.DataAnnotations;

namespace MostafaEidPortfolio.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        // قيمة بين 0.0 و 1.0 لتمثيل نسبة الإنجاز
        [Display(Name = "Progress")]
        public double Progress { get; set; }
    }
}