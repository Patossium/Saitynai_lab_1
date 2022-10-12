using Saitynai_lab_1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Saitynai_lab_1.Data.Repositories
{
    public interface IReviewsRepository
    {
        Task CreateAsync(Review review);
        Task DeleteAsync(Review review);
        Task<Review?> GetAsync(Book book, int reviewId);
        Task<IReadOnlyList<Review>> GetManyAsync(Book book);
        Task UpdateAsync(Review review);
    }
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly BooksDbContext _booksDbContext;
        public ReviewsRepository(BooksDbContext booksDbContext)
        {
            _booksDbContext = booksDbContext;
        }
        public async Task<Review?>GetAsync(Book book, int reviewId)
        {
            return await _booksDbContext.Reviews.Where(o => o.Book == book).FirstOrDefaultAsync(o => o.Id == reviewId);
        }
        public async Task<IReadOnlyList<Review>> GetManyAsync(Book book)
        {
            return await _booksDbContext.Reviews.Where(o => o.Book == book).ToListAsync();
        }
        public async Task CreateAsync(Review review)
        {
            _booksDbContext.Reviews.Add(review);
            await _booksDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Review review)
        {
            _booksDbContext.Reviews.Update(review);
            await _booksDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Review review)
        {
            _booksDbContext.Reviews.Remove(review);
            await _booksDbContext.SaveChangesAsync();
        }
    }
}
