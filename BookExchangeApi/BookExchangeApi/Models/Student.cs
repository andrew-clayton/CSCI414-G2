using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookExchangeApi.Models
{
    public class Student
    {
        [Key] public int StudentId { get; set; }
        [StringLength(20)] public string FirstName { get; set; }
        [StringLength(30)] public string LastName { get; set; }
        [ForeignKey("SchoolId")] public int SchoolId { get; set; }
        [StringLength(40)] public string FieldOfStudy { get; set; }
    }
}
