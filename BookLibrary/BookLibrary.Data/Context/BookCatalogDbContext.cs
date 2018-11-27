using System.Data.Entity;
using BookLibrary.Data.Entity;

namespace BookLibrary.Data.Context
{
    public class BookCatalogDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Writer> Writers { get; set; }
        public DbSet<Genre> Genres { get; set; }
    }
}