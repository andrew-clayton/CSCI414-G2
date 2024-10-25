using System.ComponentModel.DataAnnotations;

namespace BookExchangeApi.Models
{
    public class Book
    {
        [Key] public int BookId { get; }
        [Required][StringLength(50)] public string Title { get; set; }
        [StringLength(200)] public string Description { get; set; }
        [StringLength(200)] public string Author { get; set; }
    }
}
