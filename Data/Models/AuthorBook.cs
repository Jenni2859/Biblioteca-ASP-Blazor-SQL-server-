namespace Biblioteca.Data.Models
{
    public class AuthorBook
    {
        public string BookId { get; set; }
        public Book book { get; set; }


        public string AuthorId { get; set; }
        public Author author { get; set; }
    }
}
