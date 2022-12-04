using Saitynai_lab_1.Data.Dtos.Books;
using Saitynai_lab_1.Data.Entities;
using Saitynai_lab_1.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.AspNetCore.Authorization;
using Saitynai_lab_1.Auth.Model;

namespace Saitynai_lab_1.Controllers
{
    [ApiController]
    [Route("api/books")]

    public class BooksController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;

        public BooksController(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<BooksDto>> GetMany()
        {
            var books = await _booksRepository.GetManyAsync();
            return books.Select(o => new BooksDto(o.Id, o.Name, o.Author, o.Genre, o.Rating));
        }

        [HttpGet()]
        [Route("{bookId}", Name = "GetBook")]
        public async Task<ActionResult<BooksDto>> Get(int bookId)
        {
            var book =await _booksRepository.GetAsync(bookId);
            if(book == null)
                return NotFound();

            return new BooksDto(book.Id, book.Name, book.Author, book.Genre, book.Rating);
        }

        [HttpPost]
        [Authorize(Roles = BookRoles.Admin)]
        public async Task<ActionResult<BooksDto>> Create(CreateBooksDto createBooksDto)
        {
            var book = new Book {  Name = createBooksDto.Name, Author = createBooksDto.Author, Genre = createBooksDto.Genre, Rating = createBooksDto.Rating };

            await _booksRepository.CreateAsync(book);

            return Created("", new BooksDto(book.Id, book.Name, book.Author, book.Genre, book.Rating));
        }

        [HttpPut]
        [Route("{bookId}")]
        [Authorize(Roles = BookRoles.Admin)]
        public async Task<ActionResult<BooksDto>> Update(int bookId, UpdateBooksDto updateBooksDto)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            book.Name = updateBooksDto.Name;
            book.Author = updateBooksDto.Author;
            book.Genre = updateBooksDto.Genre;
            book.Rating = updateBooksDto.Rating;

            await _booksRepository.UpdateAsync(book);

            return Ok(new BooksDto(bookId, book.Name, book.Author, book.Genre, book.Rating));
        }

        [HttpDelete]
        [Route("{bookId}")]
        [Authorize(Roles = BookRoles.Admin)]
        public async Task<ActionResult> Remove(int bookId)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            await _booksRepository.DeleteAsync(book);

            return NoContent();
        }
    }
}
