using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookExchangeApi.Models
{
    public class Notification
    {
        [Key] public int NotificationId { get; set; }
        [Required][ForeignKey("StudentId")] public int StudentId { get; set; }
        [Required][ForeignKey("BookOfferingId")] public int BookOfferingId { get; set; }
        [Required] public NotificationType NotificationType { get; set; }
        [Required] public DateTimeOffset Timestamp { get; set; }
    }
}
