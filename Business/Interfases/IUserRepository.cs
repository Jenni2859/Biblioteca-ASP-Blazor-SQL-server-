using Biblioteca.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Biblioteca.Business.Interfaces
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task DeleteUserAsync(string userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string userId);
        Task UpdateUserAsync(User user);
    }
}
