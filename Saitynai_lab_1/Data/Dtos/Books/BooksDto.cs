namespace Saitynai_lab_1.Data.Dtos.Books
{
    public record BooksDto(int Id, string Name, string Author, string Genre, double Rating);
    public record CreateBooksDto(string Name, string Author, string Genre, double Rating);
    public record UpdateBooksDto(string Name, string Author, string Genre, double Rating);
}
