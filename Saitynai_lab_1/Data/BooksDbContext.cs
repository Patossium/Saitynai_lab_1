using Saitynai_lab_1.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Saitynai_lab_1.Auth.Model;
using Microsoft.EntityFrameworkCore;

namespace Saitynai_lab_1.Data
{
    public class BooksDbContext : IdentityDbContext<BookUser>
    {
        private readonly IConfiguration _configuration;
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewScore> ReviewScores { get; set; }

        public BooksDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=ForumDb2");
            optionsBuilder.UseNpgsql(_configuration.GetValue<string>("PostgreSQLConnectionString"));
        }
    }
}
