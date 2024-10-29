using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookExchangeApi.Models
{
    public class BookOffering
    {

        [Key] public int BookOfferingId { get; set; }
        [Required] [ForeignKey("BookId")] public int BookId { get; set; }
        [Required] public Availability AvailabilityStatus { get; set; }
        [Required] public decimal Price { get; set; }
        [Required] [ForeignKey("StudentId")] public int StudentId { get; set; }
    }
}
