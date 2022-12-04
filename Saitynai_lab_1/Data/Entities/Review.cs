using Saitynai_lab_1.Auth.Model;
using System.ComponentModel.DataAnnotations;

namespace Saitynai_lab_1.Data.Entities
{
    public class Review : IUserOwnedResource
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Book Book { get; set; }
        public int Rating { get; set; }

        [Required]

        public string UserId { get; set; }
        public BookUser User { get; set; }
    }
}
