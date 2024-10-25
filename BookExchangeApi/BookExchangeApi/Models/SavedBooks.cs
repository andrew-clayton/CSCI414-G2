 using System.ComponentModel.DataAnnotations;
 using System.ComponentModel.DataAnnotations.Schema;

 namespace BookExchangeApi.Models
{
    public class SavedBooks
    {
        [ForeignKey("BookId")] [Required] public int BookId { get; set; }
        [ForeignKey("StudentId")] [Required] public int StudentId { get; set; }
    }
}
