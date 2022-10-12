using Saitynai_lab_1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Saitynai_lab_1.Data.Repositories
{
    public interface IBooksRepository
    {
        Task CreateAsync(Book book);
        Task DeleteAsync(Book book);
        Task<Book?> GetAsync(int bookId);
        Task<IReadOnlyList<Book>> GetManyAsync();
        Task UpdateAsync(Book book);
    }

    public class BooksRepository : IBooksRepository
    {
        private readonly BooksDbContext _booksDbContext;

        public BooksRepository(BooksDbContext booksDbContext)
        {
            _booksDbContext = booksDbContext;
        }

        public async Task<Book?> GetAsync(int bookId)
        {
            return await _booksDbContext.Books.FirstOrDefaultAsync(o => o.Id == bookId);
        }
        public async Task<IReadOnlyList<Book>>GetManyAsync()
        {
            return await _booksDbContext.Books.ToListAsync();
        }
        public async Task CreateAsync(Book book)
        {
            _booksDbContext.Books.Add(book);
            await _booksDbContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Book book)
        {
            _booksDbContext.Books.Update(book);
            await _booksDbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Book book)
        {
            _booksDbContext.Books.Remove(book);
            await _booksDbContext.SaveChangesAsync(); 
        }

    }
}
