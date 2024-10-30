using Microsoft.EntityFrameworkCore;

namespace BookExchangeApi.Models
{
    public class BookExchangeContext(DbContextOptions<BookExchangeContext> options) : DbContext(options)
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BookOffering> BookOfferings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<SavedBook> SavedBooks { get; set; }

    }
}
