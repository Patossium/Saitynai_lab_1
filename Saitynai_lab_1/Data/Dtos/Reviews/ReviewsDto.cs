using Saitynai_lab_1.Data.Entities;

namespace Saitynai_lab_1.Data.Dtos.Reviews
{
    public record ReviewsDto(int Id, string Text, Book Book, int Rating, string UserId);
    public record CreateReviewsDto(string Text, int Rating);
    public record UpdateReviewsDto(string Text, int Rating);

}
