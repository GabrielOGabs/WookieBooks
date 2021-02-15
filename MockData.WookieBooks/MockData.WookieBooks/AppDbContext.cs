using Microsoft.EntityFrameworkCore;
using Models.WookieBooks;

namespace MockData.WookieBooks
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
