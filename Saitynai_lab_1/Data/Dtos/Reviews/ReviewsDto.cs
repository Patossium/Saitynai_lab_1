using Saitynai_lab_1.Data.Entities;

namespace Saitynai_lab_1.Data.Dtos.Reviews
{
    public record ReviewsDto(int Id, int UserId, string Text, Book Book, int Rating);
    public record CreateReviewsDto(int UserId, string Text, int Rating);
    public record UpdateReviewsDto(int UserId, string Text, int Rating);

}
