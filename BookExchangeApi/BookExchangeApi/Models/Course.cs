using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookExchangeApi.Models
{
    public class Course
    {
        [Key] public int CourseId { get; set; }
        [Required] public string CourseName { get; set; }
        public int CourseNumber { get; set; }
        [StringLength(150)] public string Professor { get; set; }
        [StringLength(40)] public string FieldOfStudy { get; set; }
        [ForeignKey("SchoolId")] public int SchoolId { get; set; }
        [ForeignKey("BookId")] public int BookId { get; set; }
    }
}
