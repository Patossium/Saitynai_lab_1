using Saitynai_lab_1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Saitynai_lab_1.Data.Repositories
{
    public interface IReviewScoresRepository
    {
        Task CreateAsync(ReviewScore reviewScore);
        Task DeleteAsync(ReviewScore reviewScore);
        Task<ReviewScore?> GetAsync(Review review, int reviewScoreId);
        Task<IReadOnlyList<ReviewScore>> GetManyAsync(Review review);
        Task UpdateAsync(ReviewScore reviewScore);
    }
    public class ReviewScoresRepository : IReviewScoresRepository
    {
        private readonly BooksDbContext _booksDbContext;
        public ReviewScoresRepository(BooksDbContext booksDbContext)
        {
            _booksDbContext = booksDbContext;
        }
        public async Task<ReviewScore?> GetAsync(Review review, int reviewScoreId)
        {
            return await _booksDbContext.ReviewScores.Where(o => o.Review == review).FirstOrDefaultAsync(o => o.Id == reviewScoreId);
        }
        public async Task<IReadOnlyList<ReviewScore>> GetManyAsync(Review review)
        {
            return await _booksDbContext.ReviewScores.Where(o => o.Review == review).ToListAsync();
        }
        public async Task CreateAsync (ReviewScore reviewScore)
        {
            _booksDbContext.ReviewScores.Add(reviewScore);
            await _booksDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync (ReviewScore reviewScore)
        {
            _booksDbContext.ReviewScores.Update(reviewScore);
            await _booksDbContext.SaveChangesAsync();  
        }
        public async Task DeleteAsync(ReviewScore reviewScore)
        {
            _booksDbContext.ReviewScores.Remove(reviewScore);
            await _booksDbContext.SaveChangesAsync();
        }
    }
}
