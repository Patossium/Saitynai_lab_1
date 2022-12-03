using Saitynai_lab_1.Data.Dtos.Reviews;
using Saitynai_lab_1.Data.Dtos.ReviewScores;
using Saitynai_lab_1.Data.Entities;
using Saitynai_lab_1.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Saitynai_lab_1.Controllers
{
    [ApiController]
    [Route("api/books/{bookId}/reviews/{reviewId}/reviewScores")]
    public class ReviewScoreController : ControllerBase
    {
        private readonly IBooksRepository _booksRepository;
        private readonly IReviewsRepository _reviewsRepository;
        private readonly IReviewScoresRepository _reviewsScoresRepository;

        public ReviewScoreController(IBooksRepository booksRepository, IReviewsRepository reviewsRepository, IReviewScoresRepository reviewsScoresRepository)
        {
            _booksRepository = booksRepository;
            _reviewsRepository = reviewsRepository;
            _reviewsScoresRepository = reviewsScoresRepository;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewScoresDto>>> GetMany(int bookId, int reviewId)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var review = await _reviewsRepository.GetAsync(book, reviewId);

            if (review == null)
                return NotFound();

            var reviewScores = await _reviewsScoresRepository.GetManyAsync(review);
            return Ok(reviewScores.Select(o => new ReviewScoresDto(o.Id, o.UpvoteNumber, o.DownvoteNumber, o.Review)));
        }

        [HttpGet()]
        [Route("{reviewScoreId}", Name = "ReviewScoreReview")]
        public async Task<ActionResult<ReviewScoresDto>> Get(int bookId, int reviewId, int reviewScoreId)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var review = await _reviewsRepository.GetAsync(book, reviewId);

            if (review == null)
                return NotFound();

            var reviewScore = await _reviewsScoresRepository.GetAsync(review, reviewScoreId);

            if (reviewScore == null)
                return NotFound();

            return new ReviewScoresDto(reviewScore.Id, reviewScore.UpvoteNumber, reviewScore.DownvoteNumber, reviewScore.Review);

        }

        [HttpPost]
        public async Task<ActionResult<ReviewScoresDto>> Create(int bookId, int reviewId, CreateReviewScoresDto createReviewScoresDto)
        {
            var book = _booksRepository.GetAsync(bookId);

            if (book == null || book.Result == null)
                return NotFound();

            var review = _reviewsRepository.GetAsync(book.Result, reviewId);

            if (review == null || review.Result == null)
                return NotFound();

            var reviewScore = new ReviewScore {  UpvoteNumber = createReviewScoresDto.UpvoteNumber, DownvoteNumber = createReviewScoresDto.DownvoteNumber, Review = review.Result};

            await _reviewsScoresRepository.CreateAsync(reviewScore);

            return Created("", new ReviewScoresDto(reviewScore.Id, reviewScore.UpvoteNumber, reviewScore.DownvoteNumber, reviewScore.Review));
        }

        [HttpPut]
        [Route("{reviewScoreId}")]
        public async Task<ActionResult<ReviewScoresDto>> Update(int bookId, int reviewId, int reviewScoreId, UpdateReviewScoresDto updateReviewScoresDto)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var review = await _reviewsRepository.GetAsync(book, reviewId);

            if (review == null)
                return NotFound();

            var reviewScore = await _reviewsScoresRepository.GetAsync(review, reviewScoreId);

            if (reviewScore == null)
                return NotFound();

            reviewScore.UpvoteNumber = updateReviewScoresDto.UpvoteNumber;
            reviewScore.DownvoteNumber = updateReviewScoresDto.DownvoteNumber;

            await _reviewsScoresRepository.UpdateAsync(reviewScore);

            return Ok(new ReviewScoresDto(reviewScore.Id, reviewScore.UpvoteNumber, reviewScore.DownvoteNumber, review));
        }

        [HttpDelete]
        [Route("{reviewScoreId}")]
        public async Task<ActionResult> Remove(int bookId, int reviewId, int reviewScoreId)
        {
            var book = await _booksRepository.GetAsync(bookId);

            if (book == null)
                return NotFound();

            var review = await _reviewsRepository.GetAsync(book, reviewId);

            if (review == null)
                return NotFound();

            var reviewScore = await _reviewsScoresRepository.GetAsync(review, reviewScoreId);

            if (reviewScore == null)
                return NotFound();

            await _reviewsScoresRepository.DeleteAsync(reviewScore);

            return NoContent();

        }
    }
}
