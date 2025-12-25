using Microsoft.EntityFrameworkCore;
using KutuphaneYonetim.Models;

namespace KutuphaneYonetim.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
