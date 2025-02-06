using Biblioteca.Data.Models;

namespace Biblioteca.Business.Interfaces
{
    public interface ISanctionRepository
    {
        Task<IEnumerable<Sanction>> GetAllAsync();
        Task<Sanction> GetByIdAsync(int id);
        Task AddSanctionAsync(Sanction sanction);
        Task UpdateSanctionAsync(Sanction sanction);
        Task DeleteSanctionAsync(int id);
    }
}
