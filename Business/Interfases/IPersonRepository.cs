using Biblioteca.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces
{
    public interface IPersonRepository
    {
        Task AddPersonAsync(Person person);
        Task UpdatePersonAsync(Person person);
        Task DeletePersonAsync(string personId);
        Task<Person> GetByIdAsync(string personId);
        Task<IEnumerable<Person>> GetAllAsync();
    }
}
