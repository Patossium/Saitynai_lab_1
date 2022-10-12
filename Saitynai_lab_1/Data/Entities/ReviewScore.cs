namespace Saitynai_lab_1.Data.Entities
{
    public class ReviewScore
    {
        public int Id { get; set; }
        public int UpvoteNumber { get; set; }
        public int DownvoteNumber { get; set; }
        public Review Review { get; set; }
    }
}
