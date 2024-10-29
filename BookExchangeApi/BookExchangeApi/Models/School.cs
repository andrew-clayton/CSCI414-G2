using System.ComponentModel.DataAnnotations;

namespace BookExchangeApi.Models
{
    public class School
    {
        [Key] public int SchoolId { get; set; }
        [Required][StringLength(50)] public string Name { get; set; }
        [StringLength(2)] public string StateCode { get; set; }
    }
}
