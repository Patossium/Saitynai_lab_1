using Saitynai_lab_1.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace Saitynai_lab_1.Data.Entities
{
    public class ReviewScore
    {
        public int Id { get; set; }
        public int UpvoteNumber { get; set; }
        public int DownvoteNumber { get; set; }
        public Review Review { get; set; }

        [Required]

        public string UserId { get; set; }
        public BookUser User { get; set; }

    }
}
