using Biblioteca.Data.Models;

namespace Biblioteca.Business.Interfases
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAlLAsync();
        Task<Author> GetByIdAsyns(string id);
        Task AddAuthorAdsync(Author author);
        Task UpdateAuthorAsync(Author author);

        // Esto es solo para verificar si un autor tiene una relacion con un libro
        Task<bool> IsAuthorLinkedToBooksAsync(string authorId); 
        Task DeleteAuthorAsync(string id);
    }
}
