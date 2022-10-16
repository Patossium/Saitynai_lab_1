using Saitynai_lab_1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Saitynai_lab_1.Data
{
    public class BooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewScore> ReviewScores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=BooksDb2");
        }
    }
}
