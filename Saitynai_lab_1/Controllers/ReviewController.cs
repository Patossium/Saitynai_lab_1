using Saitynai_lab_1.Data.Dtos.Reviews;
using Saitynai_lab_1.Data.Entities;
using Saitynai_lab_1.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Saitynai_lab_1.Auth.Model;
using System.Data;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Saitynai_lab_1.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/reviews")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IBooksRepository _booksRepository;
        private readonly IAuthorizationService _authorizationService;

        public ReviewsController(IReviewsRepository reviewsRepository, IBooksRepository booksRepository, IAuthorizationService authorizationService)
        {
            _reviewsRepository = reviewsRepository;
            _booksRepository = booksRepository;
            _authorizationService = authorizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewsDto>>> GetMany(int bookId)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var reviews = await _reviewsRepository.GetManyAsync(book);
            return Ok(reviews.Select(o => new ReviewsDto(o.Id, o.Text, o.Book, o.Rating)));
        }

        [HttpGet()]
        [Route("{reviewId}", Name = "GetReview")]
        public async Task<ActionResult<ReviewsDto>> Get(int bookId, int reviewId)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var review = await _reviewsRepository.GetAsync(book, reviewId);

            if (review == null)
                return NotFound();

            return new ReviewsDto(review.Id, review.Text, review.Book, review.Rating);
        }

        [HttpPost]
        [Authorize(Roles = BookRoles.ForumUser)]
        public async Task<ActionResult<ReviewsDto>> Create(int bookId, CreateReviewsDto createReviewsDto)
        {
            var book = _booksRepository.GetAsync(bookId);

            if (book == null || book.Result == null)
                return NotFound();
            var review = new Review
            {
                Text = createReviewsDto.Text,
                Book = book.Result,
                Rating = createReviewsDto.Rating,
                UserId = User.FindFirstValue(JwtRegisteredClaimNames.Sub)
            };

            await _reviewsRepository.CreateAsync(review);

            return Created("", new ReviewsDto(review.Id, review.Text, review.Book, review.Rating));
        }

        [HttpPut]
        [Route("{reviewId}")]
        [Authorize(Roles = BookRoles.ForumUser)]
        public async Task<ActionResult<ReviewsDto>> Update(int bookId, int reviewId, UpdateReviewsDto updateReviewsDto)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var review = await _reviewsRepository.GetAsync(book, reviewId);

            if (review == null)
                return NotFound();

            var authorizationResult = await _authorizationService.AuthorizeAsync(User, review, PolicyNames.ResourceOwner);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            review.Text = updateReviewsDto.Text;
            review.Rating = updateReviewsDto.Rating;

            await _reviewsRepository.UpdateAsync(review);

            return Ok(new ReviewsDto(review.Id, review.Text, book, review.Rating));
        }
        [HttpDelete]
        [Route("{reviewId}")]
        [Authorize(Roles = BookRoles.ForumUser)]
        public async Task<ActionResult> Remove(int bookId, int reviewId)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var review = await _reviewsRepository.GetAsync(book, reviewId);

            if (review == null)
                return NotFound();

            await _reviewsRepository.DeleteAsync(review);

            return NoContent();
        }
    }
}