using Biblioteca.Data.Models;

namespace Biblioteca.Business.Interfases
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAlLAsync();
        Task<Book> GetByIdAsyns(string id);
        Task AddBookAsync(Book book);
        Task UpdateBookAsync(Book book);
        Task DeleteBookAsync(string id);

        
    }
}
