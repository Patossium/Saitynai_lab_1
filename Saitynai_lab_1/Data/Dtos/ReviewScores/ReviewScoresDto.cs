using Saitynai_lab_1.Data.Entities;

namespace Saitynai_lab_1.Data.Dtos.ReviewScores
{
   public record ReviewScoresDto (int Id, int UpvoteNumber, int DownvoteNumber, Review Review);
   public record CreateReviewScoresDto (int UpvoteNumber, int DownvoteNumber);
   public record UpdateReviewScoresDto (int UpvoteNumber, int DownvoteNumber);
}
